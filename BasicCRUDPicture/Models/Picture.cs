using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BasicCRUDPicture.Models
{
    public class Picture
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Picture Name")]
        public string PicName { get; set; }
        public string Photo { get; set; }
    }
}
