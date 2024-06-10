using GVD2247A5.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GVD2247A5.Models
{
    public class EpisodeBaseViewModel : EpisodeAddViewModel
    {
        public int Id { get; set; }
       
    }


    public class EpisodeAddViewModel
    {

        public EpisodeAddViewModel()
        {
            AirDate = DateTime.Now;
        }

        [Required, MaxLength(150)]
        public string Name { get; set; }

        [Display (Name = "Season")]
        [Range(1, Int32.MaxValue)]

        public int SeasonNumber { get; set; }

        [Display (Name ="Episode")]
        [Range(1, Int32.MaxValue)]
        public int EpisodeNumber { get; set; }

        
        public string Genre { get; set; }

        [Required]
        [Display (Name = "Date Aired")]
        [DisplayFormat (DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        public DateTime AirDate { get; set; }

        [Required, MaxLength(250)]
        [Display (Name = "Image")]
        public string ImageUrl { get; set; }

        [ MaxLength(250)]
        public string Clerk { get; set; }

        public int ShowId { get; set; }
    
        public Show Show { get; set; }

        [Range(1, Int32.MaxValue)]
        public int GenreId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Premise { get; set; }
        [Display(Name="Video")]
        public HttpPostedFileBase VideoUpload { get; set; }
        public string VideoContentType { get; set; }
    }

    public class EpisodeAddFormViewModel {

        public EpisodeAddFormViewModel()
        {
            GenreList = new SelectList(new List<string>());
            AirDate = DateTime.Now;
        }
        public string ShowName { get; set; }

        [Display(Name = "Genre")]
        public SelectList GenreList { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; }

        [Display(Name = "Season")]
        [Range(1, Int32.MaxValue)]

        public int SeasonNumber { get; set; }

        [Display(Name = "Episode")]
        [Range(1, Int32.MaxValue)]
        public int EpisodeNumber { get; set; }

        public string Genre { get; set; }

        [Required]
        [Display(Name = "Date Aired")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AirDate { get; set; }

        [Required, MaxLength(250)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

      //  [MaxLength(250)]
      //  public string Clerk { get; set; }

        public int ShowId { get; set; }

     //   public Show Show { get; set; }

        //[Range(1, Int32.MaxValue)]
     //   public int GenreId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Premise { get; set; }

        [Display(Name ="Video Attachment")]
        [DataType(DataType.Upload)]
        public string VideoUpload { get; set; }
    }

    public class EpisodeWithShowNameViewModel : EpisodeBaseViewModel
    {
        [Display(Name = "Show Name")]
        public string ShowName { get; set; }
    }

    public class EpisodeVideoViewModel
    {
        public int Id { get; set; }

        [Required]
        public string VideoContentType { get; set; }
        [Required]
        public byte[] Video { get; set; }
    }
}