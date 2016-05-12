using Microsoft.AspNet.SignalR;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGC14.Web
{
    internal class UnitySignalRDependencyResolver : DefaultDependencyResolver
    {
        private readonly IUnityContainer container;

        public UnitySignalRDependencyResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.container = container.CreateChildContainer();
        }

        public override object GetService(Type serviceType)
        {
            if (this.container.IsRegistered(serviceType))
            {
                return this.container.Resolve(serviceType);
            }

            return base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            var services = base.GetServices(serviceType);

            if (this.container.IsRegistered(serviceType))
            {
                services = this.container.ResolveAll(serviceType).Concat(services);
            }

            return services;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.container.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}