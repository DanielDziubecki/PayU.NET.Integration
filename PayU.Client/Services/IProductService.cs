using System;
using System.Collections.Generic;
using PayU.Client.Models;

namespace PayU.Client.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(Guid id);
    }
}