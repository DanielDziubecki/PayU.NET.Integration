using AutoMapper;
using PayU.Client.Models;
using PayU.Client.ViewModels;

namespace PayU.Client.Common
{
    public class AutoMapperMakeOrderProfile : Profile
    {
        public AutoMapperMakeOrderProfile()
        {
            CreateMap<Product, ProductViewModel>();
        }
    }
}