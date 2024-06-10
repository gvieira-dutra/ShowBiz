using GVD2247A5.Data;
using Microsoft.Ajax.Utilities;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GVD2247A5.Models
{
    public class ShowBaseViewModel : ShowAddViewModel
    {
        public int Id { get; set; }
    }

    public class ShowAddFormViewModel : ShowAddViewModel
    {
        public ShowAddFormViewModel()
        {
            ActorList = new SelectList(new List<Actor>());
            //ActorName = new List<string>();
        }

        [Display(Name = "Genre")]
        public SelectList GenreList { get; set; }

        [Display(Name = "Actors")]
        public SelectList ActorList { get; set; }

        public string ActorName { get; set; }
    }


    public class ShowAddViewModel
    {
        public ShowAddViewModel() 
        {
            ReleaseDate= DateTime.Now;
            var Actors = new List<ActorBaseViewModel>();
        }

        [Required, MaxLength(150)]
        public string Name { get; set; }

        public string Genre { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [ MaxLength(250)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public string Coordinator { get; set; }

        [Display(Name = "Cast")]
        public ICollection<Actor> Actors { get; set; }

        public int ActorId { get; set; }

        [Range(1, Int32.MaxValue)]
        public int GenreId { get; set; }
       
        public List<int> ActorIdList { get; set; }

        [DataType(DataType.MultilineText)]
        public string Premise { get; set; }

    }

    public class ShowWithInfoViewModel : ShowBaseViewModel
    {
        public ShowWithInfoViewModel()
        {
            Episodes = new List<EpisodeBaseViewModel>();
        }
        
        public ICollection<EpisodeBaseViewModel> Episodes { get; set; }
    }

}