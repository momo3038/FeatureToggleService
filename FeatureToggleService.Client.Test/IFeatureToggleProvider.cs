using NUnit.Framework;
using System.Collections.Concurrent;

namespace FeatureToggleService.Client.Test
{
    public class FeatureToggleWebApiProviderTest
    {
        [Test]
        public void ShouldToRetreiveToggle()
        {
            var configuration = NSubstitute.Substitute.For<IFeatureToogleProviderConfiguration>();
            
            var provider = new FeatureToggleWebApiProvider(configuration);

            provider.Get(new MyToogle(provider));
        }

    }

    public interface IFeatureToogleProviderConfiguration
    {
        string WebApiUrl { get; }
    }

    //interface class FeatureToogleProviderConfiguration : IFeatureToogleProviderConfiguration
    //{
    //    public string WebApiUrl
    //    {
    //        get
    //        {
    //            return System.Configuration.ConfigurationManager
    //        }
    //    }
    //}

    public interface IFeatureToggleProvider
    {
        FeatureToggleDto Get(IFeatureToggle featureToggle);
    }

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
            FeatureToggleList.TryGetValue(featureToggle.GetType().Name, out feature);
            return feature;
            //System.Net.Http.HttpClient http = new System.Net.Http.HttpClient();
            //var response = await http.GetAsync("api/all");

            //if(response.IsSuccessStatusCode)
            //{
            //    await response.Content.ReadAsStringAsync();
            //}
        }
    }
}