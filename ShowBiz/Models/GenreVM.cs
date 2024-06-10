using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GVD2247A5.Models
{
    public class GenreBaseViewModel
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        [Display(Name = "Genre")]

        public string Name { get; set; }
    }
}