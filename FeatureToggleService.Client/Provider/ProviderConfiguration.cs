using System;

namespace FeatureToggleService.Client.Provider
{
    public class ProviderConfiguration : IProviderConfiguration
    {
        private string _webApiUrl;
        public string WebApiUrl
        {
            get
            {
                return _webApiUrl;
            }
            set
            {
                _webApiUrl = value;
            }
        }

        public bool IsInitialized()
        {
            return true;
        }
    }
}
