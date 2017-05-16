using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace WebApplication.Models
{
    public class TrumpometerEntryTableDTO : TableEntity
    {

        public TrumpometerEntryTableDTO(string id)
        {
            this.PartitionKey = "all";
            var reverseTime = String.Format("{0:D19}", DateTime.MaxValue.Ticks - DateTime.UtcNow.Ticks);
            this.RowKey = reverseTime + "-" + id;
            this.Id = id;
        }

        public TrumpometerEntryTableDTO() { }

        public string OriginalUrl { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public string Id { get; set; }
    }

}