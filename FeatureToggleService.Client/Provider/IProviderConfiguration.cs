namespace FeatureToggleService.Client.Provider
{
    public interface IProviderConfiguration
    {
        string WebApiHost { get; set; }
        string FeatureType { get; set; }
    }
}
