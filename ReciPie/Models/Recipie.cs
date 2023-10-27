﻿using System;

namespace ReciPie.Models
{
    public class Recipie
    {
        public Guid id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public string PreparationTime { get; set; }
        public string CookingTempeture { get; set; }
        public string Ingredients { get; set; }
        public string Categories { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
    }
}