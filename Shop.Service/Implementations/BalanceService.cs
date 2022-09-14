using System;
using System.Threading.Tasks;
using Shop.DAL.Interfaces;
using Shop.Domain;
using Shop.Domain.Enum;
using Shop.Service.Interfaces;

namespace Shop.Service.Implementations
{
    public class BalanceService : IBalanceService
    {
        private readonly IUserRepository _userRepository;

        public BalanceService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse> Get(string userName)
        {
            var baseResponse = new BaseResponse();
            try
            {
                var user = await _userRepository.GetByName(userName);

                baseResponse.Data = user;
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