namespace FeatureToggleService.Client.Provider
{
    public class ProviderConfiguration : IProviderConfiguration
    {
        public string WebApiUrl
        {
            get
            {
                return "http://localhost:8020";
            }
        }
    }
}
