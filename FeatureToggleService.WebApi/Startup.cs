using System.Data.SqlClient;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using DapperWrapper;
using FeatureToggleService.Db;
using Owin;

namespace FeatureToggleService.WebApi
{
    public class Startup
    {
        private const string ConnectionString = "Data Source=LAPTOP-MORGAN\\SQLEXPRESS;Initial Catalog=FeatureToggle;Integrated Security=true;";

        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(GetDbExecutor(ConnectionString)).As<IDbExecutor>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<FeatureToggleRepository>().As<IFeatureToggleRepository>().SingleInstance();

            var container = builder.Build();

            app.UseAutofacMiddleware(container);

            var config = new HttpConfiguration
            {
                DependencyResolver = new AutofacWebApiDependencyResolver(container)
            };

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

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
