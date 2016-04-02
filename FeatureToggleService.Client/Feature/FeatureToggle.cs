namespace FeatureToggleService.Client.Feature
{
    public class FeatureToggle : IFeatureToggle
    {
        protected IFeatureToggleProvider _provider;

        protected FeatureToggle(IFeatureToggleProvider provider)
        {
            this._provider = provider;
        }

        public bool IsEnable
        {
            get
            {
                var feat = _provider.Get(this);
                return feat.IsEnable;
            }
        }

        public string Name { get; internal set; }
    }
}
