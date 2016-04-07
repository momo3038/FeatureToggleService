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
            var featureToggleDtos = _provider.GetAll();
            return featureToggleDtos.SingleOrDefault(x => x.Name == featureToggle.GetType().Name);
        }
    }
}