using Microsoft.AspNetCore.Mvc;
using ShoesEShop.Handler.Infrastructures;
using ShoesEShop.Handler.Products.Commands;
using ShoesEShop.Handler.Products.Queries;
using ShoesEShop.Web.Attributes;
using ShoesEShop.Web.Filters;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ShoesEShop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ValidateFileExtensionsFilter))]
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
            => Ok(await _broker.Query(new GetAllProductsQuery()));

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            throw new NotImplementedException();
        }

        [HttpPost("bulk_insert")]
        [AllowFileExtensions(".csv", ".xlsx")]
        public async Task<IActionResult> BulkInsertProducts(IFormFile file)
        {
            var result = await _broker.Command(new BulkInsertProductsCommand {  ProductsFile = file });
            return Ok(result);
        }
    }
}
