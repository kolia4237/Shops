using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Shop.DAL.Interfaces;
using Shop.Domain;
using Shop.Domain.Entity;
using Shop.Domain.Enum;
using Shop.Domain.Helpers;
using Shop.Domain.ViewModels.Account;
using Shop.Service.Interfaces;

namespace Shop.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        
        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse> Register(RegisterViewModel model)
        {
            var baseResponse = new BaseResponse();
            try
            {
                var users = await _userRepository.Select();
                var user = await _userRepository.GetByName(model.Name);
                if (user != null)
                {
                    baseResponse.Description = "Такой пользователь уже есть";
                    return baseResponse;
                }

                user = new User()
                {
                    Name = model.Name,
                    Role = "User",
                    Password = HashPasswordHelper.HashPassowrd(model.Password),
                    Amount = 0
                };
                if (users == null || users.Count() == 0)
                {
                    user.Role = "Admin";
                }
                
                await _userRepository.Create(user);

                var result = await Authenticate(user);

                baseResponse.Data = result;
                baseResponse.Description = "Объект добавился";
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

        public async Task<BaseResponse> Login(LoginViewModel model)
        {
            var baseResponse = new BaseResponse();
            try
            {
                var user = await _userRepository.GetByName(model.Name);
                if (user == null)
                {
                    baseResponse.Description = "Пользователь не найден";
                    return baseResponse;
                }

                if (user.Password != HashPasswordHelper.HashPassowrd(model.Password))
                {
                    baseResponse.Description = "Неверный пароль";
                    return baseResponse;
                }
                
                var result = await Authenticate(user);

                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = result;
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

        public async Task<BaseResponse> ChangePassword(PasswordViewModel model)
        {
            var baseResponse = new BaseResponse();
            try
            {
                var user = await _userRepository.GetByName(model.Name);
                if (user == null)
                {
                    baseResponse.Description = "Пользователь не найден";
                    return baseResponse;
                }

                if (user.Password != HashPasswordHelper.HashPassowrd(model.OldPassword))
                {
                    baseResponse.Description = "Неверный старый пароль";
                    return baseResponse;
                }

                if (user.Password == HashPasswordHelper.HashPassowrd(model.NewPassword))
                {
                    baseResponse.Description = "Новый пароль похож на старый";
                    return baseResponse;
                }
                user.Password = HashPasswordHelper.HashPassowrd(model.NewPassword);
                await _userRepository.Update(user);
                
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

        private async Task<ClaimsIdentity> Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return id;
        }
    }
}