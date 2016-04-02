namespace FeatureToggleService.Client.Feature
{
    public class FeatureToggleWithUserRestriction : IFeatureToggle
    {
        public FeatureToggleDto FeatureToggle { get; private set; }
        private IFeatureToggleProvider _provider;

        protected FeatureToggleWithUserRestriction(IFeatureToggleProvider provider)
        {
            _provider = provider;
            FeatureToggle = new FeatureToggleDto();
        }

        public bool IsEnable(string user)
        {
            var feat = _provider.Get(this);
            if (!feat.IsEnable)
                return false;

            return feat.Users.Contains(user);
        }
    }

}
