using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoOfTheDay.WebApp.Models
{
    public class SubmitPhotoEntryViewModel
    {
        [Required]
        [Display(Name = "Photo title")]
        [DataType(DataType.Text)]
        [StringLength(150)]
        public string PhotoTitle
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Photo description")]
        [DataType(DataType.Text)]
        [StringLength(1500)]
        public string PhotoDescription
        {
            get;
            set;
        }

        [Display(Name = "Image")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image
        {
            get;
            set;
        }
    }
}