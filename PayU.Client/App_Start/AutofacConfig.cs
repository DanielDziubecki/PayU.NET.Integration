using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using PayU.Client.Common;
using PayU.Client.Services;

namespace PayU.Client
{
    public static class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(Startup).Assembly);


            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperMakeOrderProfile());
            });


            builder.RegisterInstance(mapperConfiguration.CreateMapper()).As<IMapper>().SingleInstance();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerRequest();
            builder.RegisterType<LoginService>().As<ILoginService>().InstancePerRequest();
            builder.RegisterType<PaymentService>().As<IPaymentService>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}