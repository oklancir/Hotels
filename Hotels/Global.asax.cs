using AutoMapper;
using Hotels.Dtos;
using Hotels.Models;
using NLog;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Hotels
{
    public class MvcApplication : HttpApplication
    {
        private readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            AutoMapperConfiguration.Configure();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        public class AutoMapperConfiguration
        {
            public static void Configure()
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Guest, GuestDto>().ReverseMap();
                    cfg.CreateMap<Room, RoomDto>().ReverseMap();
                    cfg.CreateMap<Reservation, ReservationDto>().ReverseMap();
                    cfg.CreateMap<Invoice, InvoiceDto>().ReverseMap();
                    cfg.CreateMap<Item, ItemDto>().ReverseMap();
                });

                Mapper.Configuration.AssertConfigurationIsValid();
            }
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            var code = (ex is HttpException httpException) ? httpException.GetHttpCode() : 500;

            if (code != 404)
            {
                Logger.Error(ex);
            }

            Response.Clear();
            Server.ClearError();

            Response.Redirect($"~/Error/Http{code}");
        }
    }
}
