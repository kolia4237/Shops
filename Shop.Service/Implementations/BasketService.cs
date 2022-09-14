using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.DAL.Interfaces;
using Shop.Domain;
using Shop.Domain.Entity;
using Shop.Domain.Enum;
using Shop.Service.Interfaces;

namespace Shop.Service.Implementations
{
    public class BasketService : IBasketService
    {
        private readonly IClothRepository _clothRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBasketRepository _basketRepository;
        
        public BasketService(IClothRepository clothRepository, IUserRepository userRepository,
            IBasketRepository basketRepository)
        {
            _clothRepository = clothRepository;
            _userRepository = userRepository;
            _basketRepository = basketRepository;
        }

        public async Task<BaseResponse> Select(string userName)
        {
            var baseResponse = new BaseResponse();
            try
            {
                var clothes = new List<Cloth>();
                var user = await _userRepository.GetByName(userName);
                if (user != null)
                {
                    var basket = await _basketRepository.Select(user.Id);
                    if (basket != null)
                    {
                        foreach (var bst in basket)
                        {
                            var item = await _clothRepository.Get(bst.ClothId);
                            clothes.Add(item);
                        }
                    }
                }

                baseResponse.Data = clothes;
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
            return baseResponse;
        }

        public async Task<BaseResponse> Add(string userName, int clothId)
        {
            var baseResponse = new BaseResponse();
            try
            {
                var user = await _userRepository.GetByName(userName);
                if (user != null)
                {
                    var cloth = await _clothRepository.Get(clothId);
                    if (cloth != null)
                    {
                        var basket = new Basket()
                        {
                            ClothId = clothId,
                            UserId = user.Id
                        };
                        await _basketRepository.Create(basket);
                    }
                }
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

        public async Task<BaseResponse> Delete(string userName, int clothId)
        {
            var baseResponse = new BaseResponse();
            try
            {
                var user = await _userRepository.GetByName(userName);
                if (user != null)
                {
                    var basket = await _basketRepository.Get(clothId);
                    if (basket != null)
                    {
                        await _basketRepository.Delete(basket);
                    }
                }
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