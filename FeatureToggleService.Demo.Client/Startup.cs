using System;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Autofac;
using Autofac.Integration.WebApi;
using FeatureToggleService.Client.Provider;
using Owin;

namespace FeatureToggleService.Demo.Client
{
    public class Startup
    {
        private Task _toggleFeaturePollingTask;

        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterInstance(new WebApiProviderInitialisation(TimeSpan.FromSeconds(10),
                new ProviderConfiguration())).SingleInstance();

            var container = builder.Build();


            app.UseAutofacMiddleware(container);

            var config = new HttpConfiguration();
            //config.EnableSwagger(c => c.SingleApiVersion("v1", "Feature Toggle Service API"))
            //      .EnableSwaggerUi();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            StartFeatureToggleService(container);

            //app.UseWebApi(config);
        }

        private void StartFeatureToggleService(IContainer container)
        {
            var webApiProviderInitialisation = container.Resolve<WebApiProviderInitialisation>();
            _toggleFeaturePollingTask = webApiProviderInitialisation.Start();
            _toggleFeaturePollingTask.Start();
        }

        public void Stop()
        {
            _toggleFeaturePollingTask.Dispose();
        }
    }
}
