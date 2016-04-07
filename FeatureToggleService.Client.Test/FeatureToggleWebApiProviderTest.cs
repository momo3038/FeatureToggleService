using System.Collections.Generic;
using FeatureToggleService.Client.Provider;
using NFluent;
using NSubstitute;
using NUnit.Framework;

namespace FeatureToggleService.Client.Test
{
    public class FeatureToggleWebApiProviderTest
    {
        [Test]
        public void ShouldToRetreiveToggleWhenProviderIsInitialized()
        {
            var configuration = Substitute.For<IInitProvider>();
            configuration.GetAll().Returns(new List<FeatureToggleDto>
            {
                new FeatureToggleDto
                {
                    Name = "MyToggle"
                }
            });
            var provider = new WebApiProvider(configuration);

            var feature = provider.Get(new MyToggle(provider));

            Check.That(feature).IsNotNull();
        }

    }
}