using PhotoOfTheDay.WebApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoOfTheDay.WebApp.Models
{
    public class PhotoEntryViewModel
    {
        public string Id
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string ImageUrl
        {
            get;
            set;
        }
    }
}