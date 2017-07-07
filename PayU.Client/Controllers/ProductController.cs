using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using PayU.Client.Models;
using PayU.Client.Services;
using PayU.Client.ViewModels;

namespace PayU.Client.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var products = productService.GetAllProducts();
            var productsDto = mapper.Map<IEnumerable<Product>,List<ProductViewModel>>(products);
            return View(productsDto);
        }
    }
}