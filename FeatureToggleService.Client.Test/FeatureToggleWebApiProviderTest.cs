using FeatureToggleService.Client.Provider;
using NSubstitute;
using NUnit.Framework;

namespace FeatureToggleService.Client.Test
{
    public class FeatureToggleWebApiProviderTest
    {
        [Test]
        public void ShouldToRetreiveToggle()
        {
            var configuration = Substitute.For<IProviderConfiguration>();

            var provider = new WebApiProvider(configuration);

             provider.Get(new MyToogle(provider));
        }

    }
}