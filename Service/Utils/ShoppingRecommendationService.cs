// שלב ראשון: הגדרת השירות הבסיסי
using System.Net.Http;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using common.DTOs;
using Repository.Entities;
using Common.DTOs;
using Repository.Interfaces;
namespace Service.Utils
{
    public class ShoppingRecommendationService
    {
        private readonly HttpClient _httpClient;
        private readonly IContext _context;
        private readonly string _googleApiKey;

        public ShoppingRecommendationService(HttpClient httpClient, IContext context, string googleApiKey)
        {
            _httpClient = httpClient;
            _context = context;
            _googleApiKey = googleApiKey;
        }

        public async Task<ShoppingRecommendationDto> GetRecommendationAsync(int userId, ShoppingPreferenceDto preference)
        {
            // שלב 1: שליפת כתובת המשתמש
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null || string.IsNullOrWhiteSpace(user.Address))
                throw new Exception("לא נמצאה כתובת למשתמש");

            string originAddress = user.Address;

            // שלב 2: שליפת החנויות והמחירים
            var stores = await _context.Stores.ToListAsync();
            var allPrices = await _context.AllProductsStores.ToListAsync();

            // שלב 3: חישוב מחיר סל לכל חנות
            var storeWithPriceList = new List<StoreWithDetails>();
            foreach (var store in stores)
            {
                double total = 0;
                bool hasAllProducts = true;

                foreach (var item in preference.ShoppingList.Items)
                {
                    var priceEntry = allPrices.FirstOrDefault(p => p.StoreId == store.Id && p.Barcode == item.Barcode);
                    if (priceEntry == null)
                    {
                        hasAllProducts = false;
                        break;
                    }
                    total += (double)priceEntry.Price * item.Quantity;
                }

                if (hasAllProducts)
                {
                    storeWithPriceList.Add(new StoreWithDetails
                    {
                        Store = store,
                        TotalPrice = total
                    });
                }
            }

            // שלב 4: שליחת קריאת Distance Matrix לפי כתובות
            var destinations = string.Join("|", storeWithPriceList
                .Select(s => Uri.EscapeDataString(s.Store.Address)));

            string url = $"https://maps.googleapis.com/maps/api/distancematrix/json" +
                         $"?origins={Uri.EscapeDataString(originAddress)}" +
                         $"&destinations={destinations}" +
                         $"&key={_googleApiKey}" +
                         $"&language=iw&units=metric";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            var elements = doc.RootElement.GetProperty("rows")[0].GetProperty("elements");

            for (int i = 0; i < storeWithPriceList.Count; i++)
            {
                var element = elements[i];
                if (element.TryGetProperty("status", out var statusProp) && statusProp.GetString() == "OK")
                {
                    storeWithPriceList[i].DistanceInMeters = element.GetProperty("distance").GetProperty("value").GetInt32();
                    storeWithPriceList[i].DistanceText = element.GetProperty("distance").GetProperty("text").GetString();
                }
                else
                {
                    storeWithPriceList[i].DistanceInMeters = int.MaxValue;
                    storeWithPriceList[i].DistanceText = "לא ידוע";
                }
            }

            // שלב 5: סינון לפי טווח (לדוג' עד 10 ק"מ לא מקוון)
            int maxDistance = preference.IsOnline ? 30000 : 10000;
            var filtered = storeWithPriceList
                .Where(s => s.DistanceInMeters <= maxDistance)
                .ToList();

            // שלב 6: החנות הכי טובה בהתאם להעדפה
            List<StoreWithDetails> recommended;
            StoreWithDetails cheapest;

            if (preference.IsOnline)
            {
                recommended = filtered.OrderBy(s => s.TotalPrice).Take(2).ToList();
                cheapest = recommended.FirstOrDefault();
            }
            else
            {
                recommended = filtered.OrderBy(s => s.DistanceInMeters).Take(3).ToList();
                cheapest = recommended.OrderBy(s => s.TotalPrice).FirstOrDefault();
            }

            return new ShoppingRecommendationDto
            {
                RecommendedStores = recommended,
                CheapestStore = cheapest
            };
        }

    }
}