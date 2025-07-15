// Controllers/PriceComparisonController.cs
using Microsoft.AspNetCore.Mvc;
using common.DTOs;
using Repository.Interfaces;
using Common.DTOs;
using Repository.Entities;

namespace SmartShoppingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceComparisonController : ControllerBase
    {
        private readonly IRepository<AllProductsStores> _productStoresRepo;
        private readonly IRepository<Stores> _storesRepo;

        public PriceComparisonController(
            IRepository<AllProductsStores> productStoresRepo,
            IRepository<Stores> storesRepo)
        {
            _productStoresRepo = productStoresRepo;
            _storesRepo = storesRepo;
        }

        [HttpPost("best-store")]
        public ActionResult<BestStoreResultDto> GetBestStore([FromBody] BasketCheckRequestDto request)
        {
            var allData = _productStoresRepo.GetAll();

            var storeSums = allData
                .Where(p => request.Items.Any(i => i.Barcode == p.Barcode))
                .GroupBy(p => p.StoreId)
                .Select(group =>
                {
                    decimal total = 0;
                    foreach (var item in request.Items)
                    {
                        var match = group.FirstOrDefault(x => x.Barcode == item.Barcode);
                        if (match != null)
                            total += (decimal)match.Price * item.Quantity;
                    }

                    return new { StoreId = group.Key, Total = total };
                })
                .OrderBy(x => x.Total)
                .FirstOrDefault();

            if (storeSums == null)
                return NotFound("No matching store found");

            var store = _storesRepo.GetById(storeSums.StoreId);
            return new BestStoreResultDto
            {
                StoreName = store.StorName,
                BranchName = store.BranchName,
                Address = store.Address,
                TotalPrice = storeSums.Total
            };
        }
    }
}
