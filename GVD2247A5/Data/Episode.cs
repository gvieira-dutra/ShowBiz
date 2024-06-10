using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace GVD2247A5.Data
{
    public class Episode
    {

        public Episode()
        {
            AirDate = DateTime.Now;
        }

        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set;  }

        public int SeasonNumber { get; set; }

        public int EpisodeNumber { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public DateTime AirDate { get; set; }

        [Required, MaxLength(250)]
        public string ImageUrl { get; set; }

        [Required, MaxLength(250)]
        public string Clerk { get; set; }

        public int ShowId { get; set; }

        public Show Show { get; set; }

        public string Premise { get; set; }

        public string VideoContentType { get; set; }

        public byte[] Video { get; set; }

    }
}