using FeatureToggleService.Client.Feature;
using FeatureToggleService.Client.Provider;
using NFluent;
using NSubstitute;
using NUnit.Framework;

namespace FeatureToggleService.Client.Test
{
    public class FeatureToggleTest
    {
        IProvider _provider;

        [SetUp]
        public void Setup()
        {
            _provider = Substitute.For<IProvider>();
        }

        [Test]
        public void ShouldGetEnableValueFromProvider()
        {
            _provider
                .Get(Arg.Any<IFeatureToggle>())
                .Returns(new FeatureToggleDto()
                {
                    IsEnable = true
                });

            var myToggle = new MyToggle(_provider);
            Check.That(myToggle.IsEnable).IsTrue();
        }

        [Test]
        public void ShouldGetNotEnableValueFromProvider()
        {
            _provider
                .Get(Arg.Any<IFeatureToggle>())
                .Returns(new FeatureToggleDto()
                {
                    IsEnable = false
                });

            var myToggle = new MyToggle(_provider);
            Check.That(myToggle.IsEnable).IsFalse();
        }

        [Test]
        public void ShouldGetNameValueFromProvider()
        {
            _provider
                .Get(Arg.Any<IFeatureToggle>())
                .Returns(new FeatureToggleDto()
                {
                    Name = "Test"
                });

            var myToggle = new MyToggle(_provider);
            Check.That(myToggle.Name).IsEqualTo("Test");
        }
    }

    public class MyToggle : FeatureToggle
    {

        public MyToggle(IProvider provider) : base(provider)
        {

        }
    }

    public class MyToogleWithUser : FeatureToggleWithUserRestriction
    {

        public MyToogleWithUser(IProvider provider) : base(provider)
        {

        }
    }

}
