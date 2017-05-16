using System;
using WebApplication.Models;

namespace WebApplication.Models
{
    public static class ModelExtensions
    {

        public static TrumpometerEntry ToModel(this TrumpometerEntryTableDTO dto)
        {

            var model = new TrumpometerEntry()
            {
                OriginalUrl = dto.OriginalUrl,
                Text = dto.Text,
                Score = dto.Score,
                Id = dto.Id
            };

            return model;

        }

        public static TrumpometerEntryTableDTO ToDTO(this TrumpometerEntry model)
        {

            var dto = new TrumpometerEntryTableDTO(model.Id)
            {
                OriginalUrl = model.OriginalUrl,
                Text = model.Text,
                Score = model.Score
            };

            return dto;

        }
    }
}