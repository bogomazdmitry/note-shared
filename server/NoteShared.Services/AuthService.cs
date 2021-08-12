using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using NoteShared.DTO;
using NoteShared.Infrastructure.Data.Entity.Users;

namespace NoteShared.Services.Interfaces
{
    public class AuthService
    {
        private readonly IRepositioryUsers _repositioryUser;

        private readonly UserManager<User> _userManager;

        public AuthService(UserManager<User> userManager, IRepositioryUsers repositioryUser)
        {
            _repositioryUser = repositioryUser;
            _userManager = userManager;
        }

        public async Task<ServiceRespose<SignUpUserRequest>> SignUp(SignUpUserRequest signUpUser)
        {
            var user = new User { Email = signUpUser.Email, UserName = signUpUser.UserName};
            var emalUnique = await CheckUniqueEmail(user.Email);
            if (!emalUnique.Success)
            {
                return new ServiceRespose<SignUpUserRequest>(emalUnique);
            }
            var userNameUnique = await CheckUniqueUserName(user.UserName);
            if (!userNameUnique.Success)
            {
                return new ServiceRespose<SignUpUserRequest>(userNameUnique);
            }
            var identityResult = await _userManager.CreateAsync(user, signUpUser.Password);
            if(!identityResult.Succeeded)
            {
                return new ServiceRespose<SignUpUserRequest>("password badVaildation");
            }
            return new ServiceRespose<SignUpUserRequest>(signUpUser);
        }

        public async Task<ServiceRespose> CheckUniqueEmail(string email)
        {
            if (_repositioryUser.GetAllByQueryable(e => e.Email == email).Any())
            {
                return new ServiceRespose("email notUnique");
            }
            return new ServiceRespose();
        }

        public async Task<ServiceRespose> CheckUniqueUserName(string userName)
        {
            if (_repositioryUser.GetAllByQueryable(e => e.UserName == userName).Any())
            {
                return new ServiceRespose("userName notUnique");
            }
            return new ServiceRespose();
        }
    }
}
