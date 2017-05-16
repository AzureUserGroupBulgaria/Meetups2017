using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoOfTheDay.WebApp.Entities
{
    public class PhotoEntryTableEntity : TableEntity
    {
        public PhotoEntryTableEntity(string id)
        {
            this.PartitionKey = "photos";
            this.RowKey = id;
        }

        public PhotoEntryTableEntity()
        {

        }

        public string Title
        {
            get;
            set;
        }

        public string ImageId
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public DateTimeOffset PublishedOn
        {
            get;
            set;
        }

        public static PhotoEntryTableEntity FromEntity(PhotoEntry entity)
        {
            var tableEntity = new PhotoEntryTableEntity(entity.Id);
            tableEntity.Title = entity.Title;
            tableEntity.Description = entity.Description;
            tableEntity.PublishedOn = entity.PublishedOn;

            return tableEntity;
        }
    }
}