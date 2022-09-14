using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.DAL.Interfaces;
using Shop.Domain;
using Shop.Domain.Entity;
using Shop.Domain.Enum;
using Shop.Domain.Extensions;
using Shop.Domain.ViewModels;
using Shop.Service.Interfaces;

namespace Shop.Service.Implementations
{
    public class ClothService : IClothService
    {
        private readonly IClothRepository _clothRepository;
        private readonly IBasketRepository _basketRepository;

        public ClothService(IClothRepository clothRepository,
            IBasketRepository basketRepository)
        {
            _clothRepository = clothRepository;
            _basketRepository = basketRepository;
        }
        
        public async Task<BaseResponse> GetClothes(int parameter)
        {
            var baseResponse = new BaseResponse();
            try
            {
                var response = await _clothRepository.Select();
                if (response != null && response.Count() != 0)
                {
                    switch (parameter)
                    {
                        case 1:
                            baseResponse.Data = response.Where(x => x.Gender == Gender.Male).ToList();
                            break;
                        case 2:
                            baseResponse.Data = response.Where(x => x.Gender == Gender.Female).ToList();
                            break;
                        default:
                            baseResponse.Data = response.ToList();
                            break;
                    }   
                }

                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse> Get(int id)
        {
            var baseResponse = new BaseResponse();
            try
            {
                var cloth = await _clothRepository.Get(id);
                if (cloth == null)
                {
                    baseResponse.StatusCode = StatusCode.NotFound;
                    baseResponse.Description = "Объект не найден";
                    return baseResponse;
                }
                
                var productViewModel = new Cloth()
                {
                    Name = cloth.Name,
                    Description = cloth.Description,
                    Price = cloth.Price,
                    Avatar = cloth.Avatar
                };
                
                baseResponse.Data = productViewModel;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse> Delete(int id)
        {
            var baseResponse = new BaseResponse();
            try
            {
                var cloth = await _clothRepository.Get(id);
                if (cloth == null)
                {
                    baseResponse.StatusCode = StatusCode.NotFound;
                    baseResponse.Description = "Объект не найден";
                    return baseResponse;
                }

                var basketItems = await _basketRepository.Select();
                foreach (var tItem in basketItems)
                {
                    await _basketRepository.Delete(tItem);
                }
                await _clothRepository.Delete(cloth);

                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Description = $"Объект {cloth.Name} удалён";
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse> Edit(int id, ProductViewModel model)
        {
            var baseResponse = new BaseResponse();
            try
            {
                if (model == null)
                {
                    baseResponse.StatusCode = StatusCode.NotFound;
                    baseResponse.Description = "Объект не найден";
                    return baseResponse;
                }

                var cloth = await _clothRepository.Get(id);
                if (cloth != null)
                {
                    cloth.Description = model.Description;
                    cloth.Gender = model.Gender;
                    cloth.Name = model.Name;
                    cloth.Price = model.Price;
                    
                    await _clothRepository.Update(cloth);
                }

                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse> Create(ProductViewModel model, byte[] imageData)
        {
            var baseResponse = new BaseResponse();
            try
            {
                if (model == null)
                {
                    baseResponse.StatusCode = StatusCode.NotFound;
                    baseResponse.Description = "Объект не найден";
                    return baseResponse;
                }
                
                var car = new Cloth()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Gender = model.Gender,
                    Price = model.Price,
                    Avatar = imageData
                }; 
                await _clothRepository.Create(car);
                
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse> GetGenders(string term)
        {
            var baseResponse = new BaseResponse();
            try
            {
                var genders = new List<Gender>()
                {
                    Gender.Female,
                    Gender.Male
                }.Select(x => new
                {
                    GetDisplayName = x.GetDisplayName()
                });

                baseResponse.Data = genders;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}