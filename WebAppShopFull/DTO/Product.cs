﻿using System.Collections.Generic;

namespace DTO
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int Price { get; set; }
        public short Quantity { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public List<Product> Relation { get; set; }
    }
}
