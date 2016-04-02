using FeatureToggleService.Client.Provider;

namespace FeatureToggleService.Client.Feature
{
    public class FeatureToggle : IFeatureToggle
    {
        protected IProvider _provider;
        private FeatureToggleDto _feature;

        protected FeatureToggle(IProvider provider)
        {
            this._provider = provider;
            this._feature = _provider.Get(this);
        }

        public bool IsEnable
        {
            get
            {
                return _feature.IsEnable;
            }
        }

        public string Name
        {
            get
            {
                return _feature.Name;
            }
        }
    }
}
