using System;
using System.Configuration;

namespace FeatureToggleService.Client.Provider
{
    public class ConfigFileConfiguration : IProviderConfiguration
    {
        public string WebApiUrl
        {
            get { return ConfigurationManager.AppSettings["FeatureToggleService.WebApiUrl"]; }
            set { throw new NotSupportedException("Unable to set WebApiUrl in ConfigFileConfiguration");}
        }
    }
}
