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

            var myToggle = new MyToogle(_provider);
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

            var myToggle = new MyToogle(_provider);
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

            var myToggle = new MyToogle(_provider);
            Check.That(myToggle.Name).IsEqualTo("Test");
        }
    }


    //public class FeatureToggleListTest
    //{
    //    IFeatureToggleProvider _provider;

    //    [SetUp]
    //    public void Setup()
    //    {
    //        _provider = Substitute.For<IFeatureToggleProvider>();

    //    }

    //    [Test]
    //    public void Should_retreive_feature_by_name_if_exist()
    //    {
    //        FeatureToggleList.Feature = new List<FeatureToggleDto>();
    //        FeatureToggleList.Feature.Add(new FeatureToggleDto() { Name = "FeatOne" });

    //        var feat = FeatureToggleList.Get(new FeatOne(_provider));
    //        Check.That(feat).IsNotNull();
    //    }

    //    public class FeatOne : FeatureToggle
    //    {
    //        public FeatOne(IFeatureToggleProvider provider) : base(provider)
    //        {

    //        }
    //    }

    //    public class FeatTwo : FeatureToggle
    //    {
    //        public FeatTwo(IFeatureToggleProvider provider) : base(provider)
    //        {

    //        }
    //    }

    //    [Test]
    //    public void Should_throw_exception_if_not_exist()
    //    {
    //        FeatureToggleList.Feature = new List<FeatureToggleDto>();
    //        FeatureToggleList.Feature.Add(new FeatureToggleDto() { Name = "FeatOne" });

    //        Action feat = () => FeatureToggleList.Get(new FeatTwo(_provider));
    //        Check.ThatCode(feat).Throws<Exception>().WithMessage("Feature toggle FeatTwo does not exist.");

    //    }
    //}

    //public static class FeatureToggleList
    //{
    //    public static List<FeatureToggleDto> Feature { get; set; }

    //    public static FeatureToggleDto Get(FeatureToggle feat)
    //    {
    //        var feature = Feature.SingleOrDefault(f => f.Name == feat.GetType().Name);

    //        if (feature == null)
    //            throw new Exception(string.Format("Feature toggle {0} does not exist.", feat.GetType().Name));

    //        return feature;
    //    }
    //}


    public class MyToogle : FeatureToggle
    {

        public MyToogle(IProvider provider) : base(provider)
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
