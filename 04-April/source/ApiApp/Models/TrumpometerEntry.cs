using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class TrumpometerEntry
    {
        public TrumpometerEntry()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        public string OriginalUrl { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int Score { get; set; }

    }

}