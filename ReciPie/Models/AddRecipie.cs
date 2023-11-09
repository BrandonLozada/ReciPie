using System;

namespace ReciPie.Models
{
    public class AddRecipie
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public string ImageCover { get; set; }
        public string PreparationTime { get; set; }
        public string CookingTempeture { get; set; }
        public string Ingredients { get; set; }
        public string Categories { get; set; }
        // public Guid UserId { get; set; }
    }
}