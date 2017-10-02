﻿using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;
//using ASPNETIdentityWithOnion.Web.Binders;
using ASPNETIdentityWithOnion.Web.Extensions;
using ASPNETIdentityWithOnion.Web.Models;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace ASPNETIdentityWithOnion.Web
{
    // Note: For instructions on enabling IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=301868
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
           // ModelBinders.Binders.Add(typeof(ShoppingCart), new CartModelBinder());
        }
    }
}