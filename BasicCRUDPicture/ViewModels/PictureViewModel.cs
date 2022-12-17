using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BasicCRUDPicture.ViewModels
{
    public class PictureViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Picture Name")]
        public string PicName { get; set; }
        public IFormFile Photo { get; set; }
    }
}
