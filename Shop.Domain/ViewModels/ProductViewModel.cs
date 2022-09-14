using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Enum;

namespace Shop.Domain.ViewModels
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "Введите название")]
        [MaxLength(40, ErrorMessage = "Название должно иметь длину меньше 40 символов")]
        [MinLength(1, ErrorMessage = "Название должно иметь длину больше 1 символов")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Введите описание")]
        [MaxLength(1000, ErrorMessage = "Название должно иметь длину меньше 1000 символов")]
        [MinLength(10, ErrorMessage = "Название должно иметь длину больше 10 символов")]
        public string Description { get; set; }
       
        [Range(0, 99999.99)]
        public decimal Price { get; set; }
        
        public Gender Gender { get; set; }
        
        public int Id { get; set; }

        public IFormFile Avatar { get; set; }
    }
}