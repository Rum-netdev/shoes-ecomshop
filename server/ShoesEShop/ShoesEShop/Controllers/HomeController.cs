using Microsoft.AspNetCore.Mvc;

namespace ShoesEShop.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return Ok("API is working!");
        }
    }
}
