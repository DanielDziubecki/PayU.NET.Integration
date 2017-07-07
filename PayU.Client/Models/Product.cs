using System;

namespace PayU.Client.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
    }
}