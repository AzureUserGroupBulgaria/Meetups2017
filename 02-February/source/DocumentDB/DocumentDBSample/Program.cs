using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentDBSample
{
    class Program
    {
        private const string EndpointUri = "https://your-endpoint.documents.azure.com:443/";
        private const string PrimaryKey = "YourPrimaryKey";
        private DocumentClient client;

        static void Main(string[] args)
        {
            Program p = new Program();
            p.GetStartedDemo().Wait();
        }

        private async Task GetStartedDemo()
        {
            this.client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
            
            var backlogItem = new BacklogItem("bk007777", "Amazing feature 1", "Core");

            // Insert
            var client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
            var collectionURI = UriFactory.CreateDocumentCollectionUri("db1", "scrum");
            await this.client.CreateDocumentAsync(collectionURI, backlogItem);

            // Query
            var queryOptions = new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true };
            var query = this.client.CreateDocumentQuery<BacklogItem>(
                collectionURI, queryOptions)
                .Where(b => b.Id == "bk007")
                .ToList()
                .FirstOrDefault();

            return;
        }
    }
}