using System;

namespace ReciPie.Models
{
    public class Recipie
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageCover { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public string PreparationTime { get; set; }
        public string CookingTempeture { get; set; }
        public string Ingredients { get; set; }
        public string Categories { get; set; }
        public string UserId { get; set; }

        // public bool IsPublished { get; set; }
        // public DateTime CreatedAt { get; set; }
    }
}