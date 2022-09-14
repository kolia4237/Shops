using System.Threading.Tasks;
using Shop.Domain;

namespace Shop.Service.Interfaces
{
    public interface IBalanceService
    {
        Task<BaseResponse> Get(string userName);
    }
}