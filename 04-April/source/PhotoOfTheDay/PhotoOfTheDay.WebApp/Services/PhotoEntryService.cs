using PhotoOfTheDay.WebApp.Entities;
using PhotoOfTheDay.WebApp.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PhotoOfTheDay.WebApp.Services
{
    public class PhotoEntryService
    {
        private HttpServerUtilityBase server;

        public PhotoEntryService(HttpServerUtilityBase server)
        {
            this.server = server;
        }

        public IEnumerable<PhotoEntryViewModel> GetPhotoEntryViews()
        {
            var containerUrl = this.GetImagesContainerUrl();

            var views = this.GetEntries()
                .OrderByDescending(l => l.PublishedOn)
                .Select(l => this.PhotoEntryViewModelFromEntity(l, containerUrl));

            return views;
        }

        private string GetImagesContainerUrl()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString;
            var imageContainerName = ConfigurationManager.AppSettings["ImagesContainerName"];
            var storage = CloudStorageAccount.Parse(connectionString);
            var blobClient = storage.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(imageContainerName);

            return container.Uri.AbsoluteUri + "/";
        }

        private PhotoEntryViewModel PhotoEntryViewModelFromEntity(PhotoEntry entity, string imageContainerUrl)
        {
            var viewModel = new PhotoEntryViewModel()
            {
                Description = entity.Description,
                Id = entity.Id,
                Title = entity.Title,
                ImageUrl = GetImageUrlForEntry(entity, imageContainerUrl, ".jpg"),
            };

            return viewModel;
        }

        private static string GetImageUrlForEntry(PhotoEntry entry, string containerUrl, string suffix)
        {
            if(entry.ImageId == null)
            {
                return "/Content/images/no-image.png";
            }

            return containerUrl + entry.ImageId + suffix;
        }

        private IEnumerable<PhotoEntry> GetEntries()
        {
            var table = this.GetEntriesTable();
            var query = new TableQuery<PhotoEntryTableEntity>();
            var entries = new List<PhotoEntry>();

            foreach (PhotoEntryTableEntity entity in table.ExecuteQuery(query))
            {
                var entry = new PhotoEntry(entity.RowKey, entity.Title, entity.Description, entity.PublishedOn);
                entry.SetImage(entity.ImageId);

                entries.Add(entry);
            }

            return entries;
        }

        private CloudTable GetEntriesTable()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString;
            var tableName = ConfigurationManager.AppSettings["EntriesTableName"];

            var storage = CloudStorageAccount.Parse(connectionString);
            var tableClient = storage.CreateCloudTableClient();
            var table = tableClient.GetTableReference(tableName);
            table.CreateIfNotExists();

            return table;
        }

        public void SaveNewEntry(PhotoEntry entry)
        {
            var tableEntity = PhotoEntryTableEntity.FromEntity(entry);

            StoreEntryEntity(tableEntity);
        }

        private static void StoreEntryEntity(PhotoEntryTableEntity tableEntity)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString;
            var tableName = ConfigurationManager.AppSettings["EntriesTableName"];

            var storage = CloudStorageAccount.Parse(connectionString);
            var tableClient = storage.CreateCloudTableClient();
            var table = tableClient.GetTableReference(tableName);
            table.CreateIfNotExists();

            var insertOperation = TableOperation.Insert(tableEntity);
            table.Execute(insertOperation);
        }
    }
}