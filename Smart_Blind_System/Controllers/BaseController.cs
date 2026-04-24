using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Smart_Blind_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected Guid GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(userIdClaim, out Guid userGuid))
            {
                return userGuid;
            }
            throw new UnauthorizedAccessException("Invalid User ID");
        }
    }
}
