using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GVD2247A5.Data
{
    public class Actor
    {

        public Actor() 
        {
            BirthDate = DateTime.Now.AddYears(-50);
            Show = new HashSet<Show>();
            ActorMediaItems = new HashSet<ActorMediaItem>();
        }

        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; }

        public string AlternateName { get; set; }

        public double? Height { get; set; }

        public DateTime BirthDate { get; set; }

        [Required, MaxLength(250)]
        public string ImageUrl { get; set; }

        [Required, MaxLength(250)]
        public string Executive { get; set; }

        public ICollection<Show> Show { get; set; }

        public string Biography { get; set; }

        public ICollection<ActorMediaItem> ActorMediaItems { get; set; }
    }
}