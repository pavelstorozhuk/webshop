using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using NoteStore.Domain.Concrete;
using NoteStore.Domain.Entities;
using NoteStore.WebUI.Infrastructure.Binders;

namespace NoteStore.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer<EFDbContext>(new DropCreateDatabaseIfModelChanges<EFDbContext>());
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}
