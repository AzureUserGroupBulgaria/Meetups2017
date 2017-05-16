using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace BlobStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            // 01 - Connect to your azure storage account
            var connectionString = ConfigurationManager.AppSettings["StorageConnectionString"];
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var client = storageAccount.CreateCloudBlobClient();

            // 02 - Create a container called "text-files"
            var container = client.GetContainerReference("text-files");
            container.CreateIfNotExists();

            // 04 - Upload SampleText.txt to a block block called "UploadedSampleText.txt"
            var bytes = File.ReadAllBytes("SampleText.txt");
            var blob = container.GetBlockBlobReference("UploadedSampleText.txt");
            blob.UploadFromByteArray(bytes, 0, bytes.Length);

            // 05 - Download "UploadedSampleText.txt"
            var downloadStream = new MemoryStream();
            blob = container.GetBlockBlobReference("UploadedSampleText.txt");
            blob.DownloadToStream(downloadStream);
            downloadStream.Position = 0;
            var reader = new StreamReader(downloadStream);

            Console.WriteLine(reader.ReadToEnd());

            Console.ReadLine();
        }
    }
}
