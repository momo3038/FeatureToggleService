using FeatureToggleService.Client.Provider;
using NFluent;
using NSubstitute;
using NUnit.Framework;

namespace FeatureToggleService.Client.Test
{
    public class FeatureToggleWebApiProviderTest
    {
        [Ignore("")]
        [Test]
        public void ShouldToRetreiveToggleWhenProviderIsInitialized()
        {
            var configuration = Substitute.For<IProviderConfiguration>();
            configuration.IsInitialized().Returns(true);
            var provider = new WebApiProvider(configuration);

            var feature = provider.Get(new MyToogle(provider));

            Check.That(feature).IsNotNull();
        }

    }
}