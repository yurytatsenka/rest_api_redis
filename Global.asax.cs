﻿using MvcApplication1.Api;
using ServiceStack;
using ServiceStack.Redis;
using ServiceStack.WebHost.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcApplication1
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            new ProteinTrackerApphost().Init();
        }
    }

    public class ProteinTrackerApphost : AppHostBase 
    {
        public ProteinTrackerApphost() : base("Protein Tracker Web Services", typeof(HelloService).Assembly) {}

        public override void Configure(Funq.Container container)
        {
            SetConfig(new EndpointHostConfig { ServiceStackHandlerFactoryPath = "api"});

            container.Register<IRedisClientsManager>(c => new PooledRedisClientManager());
            container.Register<IRepository>(c => new Repository(c.Resolve<IRedisClientsManager>()));

        }
    }
}