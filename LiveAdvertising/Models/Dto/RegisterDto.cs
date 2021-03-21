using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiveAdvertising.Models.Dto
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Введите название магазина")]
        [Display(Name = "Название компании")]
        [MaxLength(50, ErrorMessage = "Максимальная длина названия - символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [MinLength(6, ErrorMessage = "Пароль должен содержать минимум 6 символов")]
        [MaxLength(20, ErrorMessage = "Пароль может содержать максимум 20 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string RepeatPassword { get; set; }

        public bool RememberMe { get; set; }
    }
}
