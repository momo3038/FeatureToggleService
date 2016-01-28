using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
