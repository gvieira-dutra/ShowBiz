using GVD2247A5.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GVD2247A5.Models
{
    public class ActorBaseViewModel : ActorAddViewModel
    {
        public int Id { get; set; }
    }
    public class ActorWithShowInfoViewModel : ActorBaseViewModel
    {
        public ActorWithShowInfoViewModel()
        {
            Show = new List<ShowBaseViewModel>();
            ActorMediaItems = new List<ActorMediaItemBaseViewModel>();
            Photos = new List<ActorMediaItemBaseViewModel>();
            Documents = new List<ActorMediaItemBaseViewModel>();
            AudioClips = new List<ActorMediaItemBaseViewModel>();
            VideoClips = new List<ActorMediaItemBaseViewModel>();
        }
        public ICollection<ShowBaseViewModel> Show { get; set; }
        public ICollection<ActorMediaItemBaseViewModel> ActorMediaItems { get; set; }
        public List<ActorMediaItemBaseViewModel> Photos { get; set; }
        public List<ActorMediaItemBaseViewModel> Documents { get; set; }
        public List<ActorMediaItemBaseViewModel> AudioClips { get; set; }
        public List<ActorMediaItemBaseViewModel> VideoClips { get; set; }
    }

    public class ActorAddViewModel
    {
        public ActorAddViewModel()
        {
            BirthDate = DateTime.Now.AddYears(-45);
            Executive = HttpContext.Current.User.Identity.Name;

        }

        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; }

        [Display(Name = "Alternate Name")]

        public string AlternateName { get; set; }

        [Display(Name = "Height (m)")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double? Height { get; set; }

        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required, MaxLength(250)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Required, MaxLength(250)]
        public string Executive { get; set; }

        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }

    }

   
}