using Microsoft.AspNetCore.Mvc;
using ShoesEShop.Handler.Infrastructures;
using ShoesEShop.Handler.Products.Commands;
using ShoesEShop.Handler.Products.Queries;

namespace ShoesEShop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IBroker _broker;

        public ProductsController(
            IBroker broker)
        {
            _broker = broker;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(_broker.Query(new GetAllProductsQuery()));

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {

        }
    }
}
