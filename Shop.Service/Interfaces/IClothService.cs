using System.Threading.Tasks;
using Shop.Domain;
using Shop.Domain.ViewModels;

namespace Shop.Service.Interfaces
{
    public interface IClothService
    {
        Task<BaseResponse> GetClothes(int parameter);

        Task<BaseResponse> Get(int id);
        
        Task<BaseResponse> Delete(int id);

        Task<BaseResponse> Edit(int id, ProductViewModel cloth);

        Task<BaseResponse> Create(ProductViewModel cloth, byte[] imageData);

        Task<BaseResponse> GetGenders(string term);
    }
}