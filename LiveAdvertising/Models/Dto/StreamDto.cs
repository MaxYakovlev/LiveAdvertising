using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiveAdvertising.Models.Dto
{
    public class StreamDto
    {
        [Required(ErrorMessage = "Введите название трансляции")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите ссылку на трансляцию")]
        public string Source { get; set; }

        [Required(ErrorMessage = "Загрузите файл с товарами")]
        public IFormFile ProductsFile { get; set; }
    }
}
