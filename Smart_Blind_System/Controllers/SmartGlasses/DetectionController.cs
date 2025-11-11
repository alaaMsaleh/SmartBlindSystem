using Microsoft.AspNetCore.Mvc;

namespace Smart_Blind_System.API.Controllers.SmartGlasses
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetectionController : ControllerBase
    {

        //    [HttpPost]
        //    public async Task<ActionResult<ImageUploadResponse>> Detected(
        //        [FromForm] IFormFile file,
        //    [FromForm] string deviceId,
        //    [FromForm] DateTime timestamp)
        //    {
        //        // 1. استلام الصورة من Raspberry Pi (File أو Base64)
        //        if (file == null || file.Length == 0)
        //        {

        //            return BadRequest("No Image file Provieded");

        //        }
        //        // 2. تحويلها إلى MemoryStream

        //        using var memoryStream = new MemoryStream();
        //        await file.CopyToAsync(memoryStream);
        //        // 3. تمريرها لمكتبة Object Detection
        //        // 4. تجهيز النتائج (List<ObjectDetected>)
        //        // 5. إرجاع Response JSON للجهاز

        //        var response = new ImageUploadResponse
        //        {
        //            Message = "Image received successfully",
        //            DeviceId = deviceId,
        //            Timestamp = timestamp
        //        };

        //        return Ok(response);

        //    }
        //}
    }
}
