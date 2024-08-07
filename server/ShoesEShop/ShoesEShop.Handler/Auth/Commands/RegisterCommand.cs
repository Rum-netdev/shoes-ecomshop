using System.ComponentModel.DataAnnotations;
using ShoesEShop.Handler.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ShoesEShop.Handler.Infrastructures;
using ShoesEShop.Data.Entities;

namespace ShoesEShop.Handler.Auth.Commands
{
    public class RegisterCommand : ICommand<RegisterCommandResult>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 6, ErrorMessage = "Length must be between {2} to {1}")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime Dob { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }

    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, RegisterCommandResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(
            UserManager<AppUser> userManager,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<RegisterCommandResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            RegisterCommandResult result = new();
            var user = _userManager.Users.Where(t => t.NormalizedUserName == request.UserName.ToUpper() || t.NormalizedEmail == request.Email.ToUpper()).FirstOrDefault();
            if (user != null)
            {
                result.IsSucceed = false;
                result.Message = "Your registered email or username is used, please try other.";
                return result;
            }
            
            user = _mapper.Map<AppUser>(request);
            var iResult = await _userManager.CreateAsync(user);
            if(iResult.Succeeded)
            {
                result.IsSucceed = true;
                result.Message = "Signing up your account successfully";
                result.UserId = user.Id;
                return result;
            }
            else
            {
                result.IsSucceed = true;
                result.Message = iResult.Errors.First().Description;
                return result;
            }
        }
    }

    public class RegisterCommandResult : BaseResult
    {
        public int UserId { get; set; }
    }
}
