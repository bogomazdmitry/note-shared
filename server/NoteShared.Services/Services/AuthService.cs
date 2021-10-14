using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using NoteShared.DTO;
using NoteShared.Infrastructure.Data.Entity.Users;

namespace NoteShared.Services.Interfaces
{
    public class AuthService : IAuthService
    {
        private readonly IRepositioryUsers _repositioryUser;

        private readonly UserManager<User> _userManager;

        public AuthService(UserManager<User> userManager, IRepositioryUsers repositioryUser)
        {
            _repositioryUser = repositioryUser;
            _userManager = userManager;
        }

        public async Task<ServiceResponse<SignUpUserRequest>> SignUp(SignUpUserRequest signUpUser)
        {
            var user = new User { Email = signUpUser.Email, UserName = signUpUser.UserName };
            var emalUnique = await CheckUniqueEmail(user.Email);
            if (!emalUnique.Success)
            {
                return new ServiceResponse<SignUpUserRequest>(emalUnique);
            }
            var userNameUnique = await CheckUniqueUserName(user.UserName);
            if (!userNameUnique.Success)
            {
                return new ServiceResponse<SignUpUserRequest>(userNameUnique);
            }
            var identityResult = await _userManager.CreateAsync(user, signUpUser.Password);
            if (!identityResult.Succeeded)
            {
                return new ServiceResponse<SignUpUserRequest>("password badValidation");
            }
            return new ServiceResponse<SignUpUserRequest>(signUpUser);
        }

        public async Task<ServiceResponse> CheckUniqueEmail(string email)
        {
            if (_repositioryUser.GetAllByQueryable(e => e.Email == email).Any())
            {
                return new ServiceResponse("email notUnique");
            }
            return new ServiceResponse();
        }

        public async Task<ServiceResponse> CheckUniqueUserName(string userName)
        {
            if (_repositioryUser.GetAllByQueryable(e => e.UserName == userName).Any())
            {
                return new ServiceResponse("userName notUnique");
            }
            return new ServiceResponse();
        }
    }
}
