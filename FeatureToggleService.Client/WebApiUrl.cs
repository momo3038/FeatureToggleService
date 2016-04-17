using System;
using FeatureToggleService.Client.Provider;

namespace FeatureToggleService.Client
{
    public class WebApiUrl
    {
        private readonly IProviderConfiguration _configuration;

        public WebApiUrl(IProviderConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Uri Get()
        {
            var host = Sanitize(_configuration.WebApiHost);
            string webApiOption = "all";
            if (!string.IsNullOrEmpty(_configuration.FeatureType))
            {
                webApiOption = _configuration.FeatureType;
            }
            return new Uri(string.Format("{0}/{1}", host, webApiOption));
        }

        private string Sanitize(string webApiHost)
        {
            var host = webApiHost;
            if (host.EndsWith("/"))
            {
                host = webApiHost.Remove(webApiHost.Length - 1);
            }
            return host;
        }
    }
}