using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.Enum
{
    public enum Gender
    {
        [Display(Name = "Неопределен")]
        None = 1,
        [Display(Name = "Мужчина")]
        Male = 1,
        [Display(Name = "Женщина")]
        Female = 2
    }
}