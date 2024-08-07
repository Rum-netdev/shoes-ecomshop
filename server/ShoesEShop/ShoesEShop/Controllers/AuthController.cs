using Microsoft.AspNetCore.Mvc;
using ShoesEShop.Handler.Auth.Commands;
using ShoesEShop.Handler.Infrastructures;

namespace ShoesEShop.Web.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IBroker _broker;
        public AuthController(
            IBroker broker)
        {
            _broker = broker;
        }


        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _broker.Command(command);
            return result.IsSucceed ? Ok(result) : BadRequest(result);
        }

        [HttpPost("/signup")]
        public async Task<IActionResult> SignUp(RegisterCommand command)
        {
            var result = await _broker.Command(command);
            return result.IsSucceed ? Ok(result) : BadRequest(result);
        }
    }
}
