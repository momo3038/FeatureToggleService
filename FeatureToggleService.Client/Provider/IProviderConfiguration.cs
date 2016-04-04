namespace FeatureToggleService.Client.Provider
{
    public interface IProviderConfiguration
    {
        string WebApiUrl { get; set; }
        bool IsInitialized();
    }
}
