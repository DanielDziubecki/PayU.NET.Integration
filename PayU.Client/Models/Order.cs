using System.Collections.Generic;

namespace PayU.Client.Models
{
    public class Order
    {
        public IEnumerable<Product> Products { get ; set; }
    }
}