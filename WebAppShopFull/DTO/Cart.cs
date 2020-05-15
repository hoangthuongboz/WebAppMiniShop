using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Cart
    {
        public long Id { get; set; }
        public int ProductId { get; set; }
        public short Quantity { get; set; }

        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
    }
}
