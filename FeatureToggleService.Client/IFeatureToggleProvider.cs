namespace FeatureToggleService.Client
{
    public interface IFeatureToggleProvider
    {
        FeatureToggleDto Get(IFeatureToggle featureToggle);
    }
}
