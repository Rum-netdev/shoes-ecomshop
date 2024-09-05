using Microsoft.AspNetCore.Mvc;
using ShoesEShop.Handler.Brands.Commands;
using ShoesEShop.Handler.Brands.Queries;
using ShoesEShop.Handler.Infrastructures;
using ShoesEShop.Web.Attributes;
using ShoesEShop.Web.Filters;

namespace ShoesEShop.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(ValidateFileExtensionsFilter))]
    public class BrandsController : ControllerBase
    {
        private readonly IBroker _broker;

        public BrandsController(IBroker broker)
        {
            _broker = broker;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
            => Ok(await _broker.Query(new GetAllBrandsQuery()));

        [HttpGet("{brandId}")]
        public async Task<IActionResult> GetById(string brandId)
        {
            return Ok(await _broker.Query(new GetBrandByIdQuery(int.Parse(brandId))));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody]CreateBrandCommand request)
        {
            return Ok(await _broker.Command(request));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBrand([FromQuery]string brandId)
            => Ok(await _broker.Command(new DeleteBrandCommand(int.Parse(brandId))));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(UpdateBrandCommand request)
        {
            return Ok(await _broker.Command(request));
        }

        [HttpPost("bulk_insert")]
        [AllowFileExtensions(".csv", ".json")]
        public async Task<IActionResult> BulkInsertBrands(IFormFile file)
        {
            var result = await _broker.Command(new BulkInsertBrandsCommand { BrandsFile = file });
            return result.IsSucceed ?
                Ok(result) :
                BadRequest(result);
        }


    }
}
