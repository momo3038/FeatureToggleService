using System;
using System.Linq;

namespace FeatureToggleService.Client.Provider
{
    public class WebApiProvider : IProvider
    {
        private readonly IInitProvider _provider;

        public WebApiProvider(IInitProvider provider)
        {
            _provider = provider;
        }

        public FeatureToggleDto Get(IFeatureToggle featureToggle)
        {
            return Get(featureToggle.GetType().Name);
        }

        public FeatureToggleDto Get(string featureToggleName, bool caseSensitive = true)
        {
            var featureToggleDtos = _provider.GetAll();
            if (caseSensitive)
            {
                return featureToggleDtos.SingleOrDefault(x => x.Name == featureToggleName);
            }
            return featureToggleDtos.SingleOrDefault(x => x.Name.ToLowerInvariant() == featureToggleName.ToLowerInvariant());
        }
    }
}