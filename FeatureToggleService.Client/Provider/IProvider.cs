namespace FeatureToggleService.Client.Provider
{
    public interface IProvider
    {
        FeatureToggleDto Get(IFeatureToggle featureToggle);
    }
}
