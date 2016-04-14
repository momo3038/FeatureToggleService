using FeatureToggleService.Client.Provider;

namespace FeatureToggleService.Client.Feature
{
    public class FeatureToggleWithUserRestriction : IFeatureToggle
    {
        private readonly IProvider _provider;

        protected FeatureToggleWithUserRestriction(IProvider provider)
        {
            _provider = provider;
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
