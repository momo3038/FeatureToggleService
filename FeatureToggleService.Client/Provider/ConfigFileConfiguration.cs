using System;
using System.Configuration;

namespace FeatureToggleService.Client.Provider
{
    public class ConfigFileConfiguration : IProviderConfiguration
    {
        public string WebApiHost
        {
            get { return ConfigurationManager.AppSettings["FeatureToggleService.WebApiHost"]; }
            set { throw new NotSupportedException("Unable to set WebApiHost in ConfigFileConfiguration"); }
        }

        public string FeatureType
        {
            get
            {
                return ConfigurationManager.AppSettings["FeatureToggleService.FeatureType"] ?? string.Empty;
            }
            set { throw new NotSupportedException("Unable to set FeatureType in ConfigFileConfiguration"); }
        }
    }
}
