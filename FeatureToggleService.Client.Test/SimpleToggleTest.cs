using FeatureToggleService.Client.Provider;
using NFluent;
using NSubstitute;
using NUnit.Framework;

namespace FeatureToggleService.Client.Test
{
    public class SimpleToggleTest
    {
        IProvider _provider;

        [SetUp]
        public void Setup()
        {
            _provider = Substitute.For<IProvider>();
        }

        [Test]
        public void ShouldGetFeatureConfiguration()
        {
            _provider
                .Get(Arg.Any<IFeatureToggle>())
                .Returns(new FeatureToggleDto()
                {
                    IsEnable = true
                });

            var myToggle = new MyToggle(_provider);
            Check.That(myToggle.IsEnable).IsTrue();
            _provider.Received(1).Get(Arg.Any<IFeatureToggle>());
        }
    }

   
}
