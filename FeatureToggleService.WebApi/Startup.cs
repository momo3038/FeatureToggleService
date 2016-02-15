using System.Data.SqlClient;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Autofac;
using Autofac.Integration.WebApi;
using DapperWrapper;
using FeatureToggleService.Data;
using Owin;
using Swashbuckle.Application;
using Microsoft.Owin.Cors;

namespace FeatureToggleService.WebApi
{
    public class Startup
    {
        private string ConnectionString = WebApiConfiguration.SqlServerConnectionString;

        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(GetDbExecutor(ConnectionString)).As<IDbExecutor>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<SqlServerRepository>().As<IFeatureToggleRepository>().SingleInstance();
            builder.RegisterType<FeatureToggleValidator>().As<IFeatureToggleValidator>().SingleInstance();

            var container = builder.Build();

            app.UseCors(CorsOptions.AllowAll);
            app.UseAutofacMiddleware(container);

            var config = new HttpConfiguration();
            config.EnableSwagger(c => c.SingleApiVersion("v1", "Feature Toggle Service API"))
                  .EnableSwaggerUi();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Services.Replace(typeof(IExceptionHandler), new UnhandledErrorService());

            app.Use(typeof(AuditMiddlware));
            app.UseWebApi(config);
        }

        private IDbExecutor GetDbExecutor(string connectionString)
        {
            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            return new SqlExecutor(sqlConnection);
        }
    }
}
