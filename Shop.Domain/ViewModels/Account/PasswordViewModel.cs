using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.ViewModels.Account
{
    public class PasswordViewModel
    {
        public string Name { get; set; }
        
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Укажите старый пароль")]
        public string OldPassword { get; set; }
        
        
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Укажите новый пароль")]
        [MinLength(6, ErrorMessage = "Пароль должен иметь длину больше 6 символов")]
        public string NewPassword { get; set; }
    }
}