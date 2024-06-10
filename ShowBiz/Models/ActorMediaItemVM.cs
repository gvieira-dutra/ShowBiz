using System.ComponentModel.DataAnnotations;
using System.Web;

namespace GVD2247A5.Models
{
    public class ActorMediaItemBaseViewModel
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string ContentType { get; set; }
    }

    public class ActorMediaItemWithContentViewModel : ActorMediaItemBaseViewModel
    {
        public byte[] Content { get; set; }
    }

    public class ActorMediaItemAddFormViewModel
    {
        public int ActorId { get; set; }
        public string ActorName { get; set; }

        [Required]
        public string Caption { get; set; }

        [Required]
        [Display(Name = "Attachment")]
        [DataType(DataType.Upload)]
        public string ContentUpload { get; set; }
    }

    public class ActorMediaItemAddViewModel
    {
        [Required]
        public HttpPostedFileBase ContentUpload { get; set; }

        [Required]
        public string Caption { get; set; }

        public int ActorId { get; set; }
    }
}