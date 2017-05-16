using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoOfTheDay.WebApp.Entities
{
    public class ImageProcessingRequest
    {
        public ImageProcessingRequest(string photoEntryId, string imageName)
        {
            this.PhotoEntryId = photoEntryId;
            this.ImageName = imageName;
        }

        public string PhotoEntryId
        {
            get;
            private set;
        }

        public string ImageName
        {
            get;
            private set;
        }
    }
}