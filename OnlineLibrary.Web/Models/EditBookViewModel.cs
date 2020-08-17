using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Web.Models
{
    public class EditBookViewModel : AddBookViewModel
    {
        [Required]
        public int Id { get; set; }
        
    }
}
