using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moq;
using Ninject;
using NoteStore.Domain.Abstract;
using NoteStore.Domain.Entities;
using NoteStore.Domain.Concrete;
using System.Data.Entity;
using NoteStore.WebUI.Models;
using NoteStore.WebUI.Infrastructure.Abstract;
using NoteStore.WebUI.Infrastructure.Concrete;
using System.Configuration;
namespace NoteStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
          

           
            kernel.Bind<INoteRepository>().To<EFNoteRepository>();
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                    .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
            kernel.Bind<IAuthProvider>().To<FormAuthProvider>();
        }
    }
}