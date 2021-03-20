using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiveAdvertising.Models.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Введите название магазина")]
        [Display(Name = "Название компании")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
