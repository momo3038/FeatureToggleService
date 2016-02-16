using System.Configuration;

namespace FeatureToggleService
{
    public static class WebApiConfiguration
    {
        public static string OwinHost
        {
            get
            {
                return ConfigurationManager.AppSettings["owin.host"];
            }
        }

        public static string SqlServerConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings["repository.sqlserver.connectionstring"];
            }
        }
    }
}
