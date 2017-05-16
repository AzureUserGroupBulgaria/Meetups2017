using PhotoOfTheDay.WebApp.Entities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace PhotoOfTheDay.WebApp.Services
{
    public class ImageProcessingService
    {
        private HttpServerUtilityBase server;
        private CloudStorageAccount storage;

        public ImageProcessingService(HttpServerUtilityBase server)
        {
            this.server = server;

            var connectionString = ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString;
            this.storage = CloudStorageAccount.Parse(connectionString);
        }

        public void ProcessImage(Stream imageStream, PhotoEntry entry)
        {
            var imageName = Guid.NewGuid().ToString();

            this.UploadImageToStorage(imageStream, imageName);
            this.SubmitImageForProcessing(entry.Id, imageName);
        }

        private void UploadImageToStorage(Stream imageStream, string imageName)
        {
            var blobClient = storage.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("incoming");
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            
            var blob = container.GetBlockBlobReference(imageName);
            blob.UploadFromStream(imageStream);
        }

        private void SubmitImageForProcessing(string entryId, string imageName)
        {
            var processQueueName = ConfigurationManager.AppSettings["ImageProcessQueueName"];
            var queueClient = this.storage.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference(processQueueName);
            queue.CreateIfNotExists();

            var queueMessage = new { EntryId = entryId, ImageId = imageName };
            var queueText = JsonConvert.SerializeObject(queueMessage);
            var message = new CloudQueueMessage(queueText);

            queue.AddMessage(message);
        }
    }
}