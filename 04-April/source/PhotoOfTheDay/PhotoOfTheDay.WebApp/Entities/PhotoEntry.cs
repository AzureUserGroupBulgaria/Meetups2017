using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoOfTheDay.WebApp.Entities
{
    public class PhotoEntry
    {
        public PhotoEntry(string id, string title, string description, DateTimeOffset publishedOn)
        {
            if(string.IsNullOrEmpty(id))
            {
                throw new NullReferenceException("id");
            }

            if (string.IsNullOrEmpty(title))
            {
                throw new NullReferenceException("title");
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new NullReferenceException("description");
            }

            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.PublishedOn = publishedOn;
        }

        public static PhotoEntry New(string title, string description)
        {
            var newId = Guid.NewGuid().ToString();
            var listing = new PhotoEntry(newId, title, description, DateTimeOffset.Now);

            return listing;
        }

        public string Id
        {
            get;
            set;
        }

        public string ImageId
        {
            get;
            private set;
        }

        public string Title
        {
            get;
            private set;
        }

        public string Description
        {
            get;
            private set;
        }

        public DateTimeOffset PublishedOn
        {
            get;
            private set;
        }

        public void SetImage(string image)
        {
            this.ImageId = image;
        }
    }
}