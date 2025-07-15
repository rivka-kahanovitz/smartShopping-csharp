//using HtmlAgilityPack;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Net;
//using System.Threading.Tasks;
//namespace Service.Utils
//{

//    public interface IImageDownloader
//    {
//        Task DownloadImagesFromCategoryAsync(string categoryUrl, string outputFolder);
//    }

//    public class ImageDownloader : IImageDownloader
//    {
//        public async Task DownloadImagesFromCategoryAsync(string categoryUrl, string outputFolder)
//        {
//            Directory.CreateDirectory(outputFolder);
//            var productUrls = await GetProductUrls(categoryUrl);

//            foreach (var productUrl in productUrls)
//            {
//                Console.WriteLine($"מעבד מוצר: {productUrl}");
//                await DownloadProductImage(productUrl, outputFolder);
//            }

//            Console.WriteLine("ההורדה הסתיימה.");
//        }

//        private async Task<List<string>> GetProductUrls(string categoryUrl)
//        {
//            var productUrls = new List<string>();
//            var web = new HtmlWeb();
//            var doc = await Task.Run(() => web.Load(categoryUrl));

//            var nodes = doc.DocumentNode.SelectNodes("//a[contains(@class, 'product-item-link')]");
//            if (nodes != null)
//            {
//                foreach (var node in nodes)
//                {
//                    string href = node.GetAttributeValue("href", "");
//                    if (!string.IsNullOrEmpty(href))
//                        productUrls.Add(href);
//                }
//            }
//            Console.WriteLine($"נמצאו {productUrls.Count} מוצרים.");

//            return productUrls;
//        }

//        private async Task DownloadProductImage(string productUrl, string outputFolder)
//        {
//            var web = new HtmlWeb();
//            var doc = await Task.Run(() => web.Load(productUrl));

//            var nameNode = doc.DocumentNode.SelectSingleNode("//h1[contains(@class, 'product-name')]");
//            string productName = nameNode?.InnerText.Trim() ?? "product";

//            var imgNode = doc.DocumentNode.SelectSingleNode("//img[contains(@class, 'product-image-photo')]");
//            string imgUrl = imgNode?.GetAttributeValue("src", "");

//            if (!string.IsNullOrEmpty(imgUrl))
//            {
//                string safeName = string.Join("_", productName.Split(Path.GetInvalidFileNameChars()));
//                string filePath = Path.Combine(outputFolder, safeName + ".jpg");

//                using (WebClient client = new WebClient())
//                {
//                    await client.DownloadFileTaskAsync(imgUrl, filePath);
//                    Console.WriteLine($"תמונה נשמרה: {filePath}");
//                }
//            }
//            else
//            {
//                Console.WriteLine("לא נמצאה תמונה למוצר: " + productName);
//            }
//        }
//    }
//}
