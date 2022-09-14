using System.Threading.Tasks;
using Shop.Domain;
using Shop.Domain.ViewModels.Account;

namespace Shop.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse> Register(RegisterViewModel model);

        Task<BaseResponse> Login(LoginViewModel model);

        Task<BaseResponse> ChangePassword(PasswordViewModel model);
    }
}