using System.Collections.Concurrent;
using System.Net.Http;

namespace FeatureToggleService.Client.Providers
{
    public class FeatureToggleWebApiProvider : IFeatureToggleProvider
    {
        private IFeatureToogleProviderConfiguration _configuration;

        public ConcurrentDictionary<string, FeatureToggleDto> FeatureToggleList { get; set; }

        public FeatureToggleWebApiProvider(IFeatureToogleProviderConfiguration configuration)
        {
            _configuration = configuration;
            FeatureToggleList = new ConcurrentDictionary<string, FeatureToggleDto>();
        }

        public FeatureToggleDto Get(IFeatureToggle featureToggle)
        {
            FeatureToggleDto feature;
            if (FeatureToggleList.TryGetValue(featureToggle.GetType().Name, out feature))
            {
                return feature;
            }
            else
            {
                HttpClient http = new HttpClient();
                //var response = await http.GetAsync("api/all");

                //if (response.IsSuccessStatusCode)
                //{
                //await response.Content.ReadAsStringAsync();

                //}
                return new FeatureToggleDto();
            }
        }
    }
}
