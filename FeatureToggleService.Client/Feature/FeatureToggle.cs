using System;
using FeatureToggleService.Client.Provider;

namespace FeatureToggleService.Client.Feature
{
    public class FeatureToggle : IFeatureToggle
    {
        private readonly IProvider _provider;
        private readonly Lazy<FeatureToggleDto> _feature;

        protected FeatureToggle(IProvider provider)
        {
            _provider = provider;
            _feature = new Lazy<FeatureToggleDto>(() => _provider.Get(this));
        }

        public bool IsEnable
        {
            get
            {
                return _feature.Value.IsEnable;
            }
        }

        public string Name
        {
            get
            {
                return _feature.Value.Name;
            }
        }
    }
}
