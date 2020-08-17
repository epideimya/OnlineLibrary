using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Web.Models
{
    public class AddBookViewModel //Только для передачи в контроллер или на вьюху
    {
        [DisplayName("Название")]
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [DisplayName("Автор")]
        [Required]
        [MaxLength(150)]
        public string Author { get; set; }

        [DisplayName("Краткое описание")]
        [Required]
        [MaxLength(500)]
        public string ShortDescription { get; set; }

        [DisplayName("Содержание")]
        [Required]
        [MaxLength(1000000)]
        public string Body { get; set; }

        [DisplayName("Год издания")]
        [Required]
        public int ReleaseYear { get; set; }

        [DisplayName("Обложка")]
        public IFormFile BookImage { get; set; }
    }
}
