using System.Threading.Tasks;
using Shop.Domain;

namespace Shop.Service.Interfaces
{
    public interface IBasketService
    {
        Task<BaseResponse> Select(string userName);

        Task<BaseResponse> Add(string userName, int clothId);
        
        Task<BaseResponse> Delete(string userName, int clothId);
    }
}