using FeatureToggleService.Client.Providers;
using NSubstitute;
using NUnit.Framework;

namespace FeatureToggleService.Client.Test
{
    public class FeatureToggleWebApiProviderTest
    {
        [Test]
        public void ShouldToRetreiveToggle()
        {
            var configuration = Substitute.For<IFeatureToogleProviderConfiguration>();

            var provider = new FeatureToggleWebApiProvider(configuration);

             provider.Get(new MyToogle(provider));
        }

    }
}