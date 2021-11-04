using NoteShared.DTO;
using System.Threading.Tasks;

namespace NoteShared.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<SignUpUserRequest>> SignUp(SignUpUserRequest signUpUser);

        Task<ServiceResponse> CheckUniqueEmail(string email);

        Task<ServiceResponse> CheckUniqueUserName(string userName);
    }
}
