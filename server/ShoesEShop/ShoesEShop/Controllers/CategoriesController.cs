using Microsoft.AspNetCore.Mvc;
using ShoesEShop.Handler.Categories.Commands;
using ShoesEShop.Handler.Infrastructures;

namespace ShoesEShop.Web.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IBroker _broker;

        public CategoriesController(IBroker broker)
        {
            _broker = broker;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand request)
        {
            return Ok(await _broker.Command(request));
        }
    }
}
