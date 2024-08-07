using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ShoesEShop.Data.Entities;
using ShoesEShop.Handler.Infrastructures;
using ShoesEShop.Handler.Services.Interfaces;
using ShoesEShop.Handler.Shared;
using ShoesEShop.Handler.Shared.Dtos;

namespace ShoesEShop.Handler.Auth.Commands
{
    public class LoginCommand : ICommand<LoginCommandResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public bool RememberMe { get; set; }
    }

    public class LoginCommandHandler : ICommandHandler<LoginCommand, LoginCommandResult>
    {
        private readonly IJwtAuthenticationManager _jwtManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public LoginCommandHandler(
            IJwtAuthenticationManager jwtManager,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IMapper mapper)
        {
            _jwtManager = jwtManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            LoginCommandResult result = new();

            var user = await _userManager.FindByNameAsync(request.UserName);
            if(user == null)
            {
                result.IsSucceed = false;
                result.Message = "Your account is not exist, please try again.";
                return result;
            }

            var loginResult = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, lockoutOnFailure: false);
            if(loginResult.Succeeded)
            {
                SetSuccessAccessToken(user, result);
                return result;
            }
            else if(loginResult.IsLockedOut)
            {
                result.IsSucceed = false;
                result.Message = "LockedOut";
                return result;
            }
            else if(loginResult.RequiresTwoFactor)
            {
                result.IsSucceed = false;
                result.Message = "RequiresTwoFactor";
                return result;
            }
            else if(loginResult.IsNotAllowed)
            {
                result.IsSucceed = false;
                result.Message = "IsNotAllowed";
                return result;
            }

            result.IsSucceed = false;
            result.Message = "Your provided password is incorrect.";
            return result;
        }

        private void SetSuccessAccessToken(AppUser user, LoginCommandResult result)
        {
            AppUserDto userDto = _mapper.Map<AppUserDto>(user);
            result.TokenAuth = _jwtManager.GenerateToken(userDto);
            result.UserId = userDto.UserId;
            result.IsSucceed = true;
        }
    }

    public class LoginCommandResult : BaseResult
    {
        public string TokenAuth { get; set; }
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
    }
}
