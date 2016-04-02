namespace FeatureToggleService.Client
{
    public class FeatureToogleProviderConfiguration : IFeatureToogleProviderConfiguration
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
