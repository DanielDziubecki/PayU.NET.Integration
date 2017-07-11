using System;
using System.Collections.Generic;
using System.Linq;
using PayU.Client.Models;

namespace PayU.Client.Services
{
    public class ProductService : IProductService
    {
        private readonly IEnumerable<Product> products = new List<Product>
        {
            new Product
            {
                Id = Guid.Parse("E83C46DC-CBA0-4D8E-9B0B-8FCC23365571"),
                Name = "Phone",
                Price = "235"
            },
            new Product
            {
                Id = Guid.Parse("4941E719-1AF9-4591-8BEC-A9F5391FF364"),
                Name = "PC",
                Price = "50"
            },
            new Product
            {
                Id =Guid.Parse("2EDA8427-E858-4813-B626-DB7EBA8FF8FB"),
                Name = "GPU",
                Price = "80"
            },
            new Product
            {
                Id = Guid.Parse("05242A03-637C-48CC-80A9-393365F5F973"),
                Name = "Keyboard",
                Price = "10"
            }
        };


        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public Product GetProductById(Guid id)
        {
            return products.Single(x => x.Id == id);
        }
    }
}