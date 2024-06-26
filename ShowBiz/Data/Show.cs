﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GVD2247A5.Data
{
    public class Show
    {   
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; }

        [Required, MaxLength(150)]
        public string Genre { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required, MaxLength(250)]
        public string ImageUrl { get; set; }

        [Required, MaxLength(250)]
        public string Coordinator { get; set; }

        public ICollection<Actor> Actors { get; set; }

        public ICollection<Episode> Episodes { get; set;}

        public string Premise { get; set; }
    }
}