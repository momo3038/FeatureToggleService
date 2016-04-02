using System.Collections.Concurrent;

namespace FeatureToggleService.Client.Provider
{
    public class WebApiProvider : IProvider
    {
        private IProviderConfiguration _configuration;

        public ConcurrentDictionary<string, FeatureToggleDto> FeatureToggleList { get; set; }

        public WebApiProvider(IProviderConfiguration configuration)
        {
            _configuration = configuration;
            FeatureToggleList = new ConcurrentDictionary<string, FeatureToggleDto>();
        }

        public FeatureToggleDto Get(IFeatureToggle featureToggle)
        {
            FeatureToggleDto feature;
            if (FeatureToggleList.TryGetValue(featureToggle.GetType().Name, out feature))
            {
                return feature;
            }
            return null;
        }
    }
}
