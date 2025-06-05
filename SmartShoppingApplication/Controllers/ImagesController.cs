using Microsoft.AspNetCore.Mvc;
using Service.Utils;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IImageDownloader _downloader;

    public ImagesController(IImageDownloader downloader)
    {
        _downloader = downloader;
    }

    [HttpGet("download")]
    public async Task<IActionResult> Download()
    {
        string categoryUrl = "https://www.rami-levy.co.il/he/online/market/חטיפים_ומוצרי_אפיה";
        string outputFolder = @"C:\Users\user1\Desktop\לימודים\שנה ב\C#\פרויקט מ 3.6\Images";

        await _downloader.DownloadImagesFromCategoryAsync(categoryUrl, outputFolder);
        return Ok("ההורדה הסתיימה.");
    }
}
