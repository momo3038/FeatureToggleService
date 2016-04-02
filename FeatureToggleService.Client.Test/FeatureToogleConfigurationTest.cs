using FeatureToggleService.Client.Feature;
using NFluent;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureToggleService.Client.Test
{
    public class FeatureToogleConfigurationTest
    {
        IFeatureToggleProvider _provider;

        [SetUp]
        public void Setup()
        {
            _provider = Substitute.For<IFeatureToggleProvider>();
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

            var myToggle = new MyToogle(_provider);
            Check.That(myToggle.IsEnable).IsTrue();
        }

        [Test]
        public void ShouldGetFeatureConfigurationWithUserRestriction()
        {
            _provider
                .Get(Arg.Any<IFeatureToggle>())
                .Returns(new FeatureToggleDto()
                {
                    IsEnable = true,
                    Users = new List<string>() { "john.doe@test.com","other.login" }
                });

            var myToggle = new MyToogleWithUser(_provider);
            Check.That(myToggle.IsEnable("john.doe@test.com")).IsTrue();
        }
    }

    public class FeatureToggleListTest
    {
        IFeatureToggleProvider _provider;

        [SetUp]
        public void Setup()
        {
            _provider = Substitute.For<IFeatureToggleProvider>();

        }

        [Test]
        public void Should_retreive_feature_by_name_if_exist()
        {
            FeatureToggleList.Feature = new List<FeatureToggleDto>();
            FeatureToggleList.Feature.Add(new FeatureToggleDto() { Name = "FeatOne" });

            var feat = FeatureToggleList.Get(new FeatOne(_provider));
            Check.That(feat).IsNotNull();
        }

        public class FeatOne : FeatureToggle
        {
            public FeatOne(IFeatureToggleProvider provider) : base(provider)
            {

            }
        }

        public class FeatTwo : FeatureToggle
        {
            public FeatTwo(IFeatureToggleProvider provider) : base(provider)
            {

            }
        }

        [Test]
        public void Should_throw_exception_if_not_exist()
        {
            FeatureToggleList.Feature = new List<FeatureToggleDto>();
            FeatureToggleList.Feature.Add(new FeatureToggleDto() { Name = "FeatOne" });

            Action feat = () => FeatureToggleList.Get(new FeatTwo(_provider));
            Check.ThatCode(feat).Throws<Exception>().WithMessage("Feature toggle FeatTwo does not exist.");

        }
    }

    public static class FeatureToggleList
    {
        public static List<FeatureToggleDto> Feature { get; set; }

        public static FeatureToggleDto Get(FeatureToggle feat)
        {
            var feature = Feature.SingleOrDefault(f => f.Name == feat.GetType().Name);

            if (feature == null)
                throw new Exception(string.Format("Feature toggle {0} does not exist.", feat.GetType().Name));

            return feature;
        }
    }


    public class MyToogle : FeatureToggle
    {

        public MyToogle(IFeatureToggleProvider provider) : base(provider)
        {

        }
    }

    public class MyToogleWithUser : FeatureToggleWithUserRestriction
    {

        public MyToogleWithUser(IFeatureToggleProvider provider) : base(provider)
        {

        }
    }



    public class Test
    {
        IFeatureToggleProvider _provider;
        public Test(IFeatureToggleProvider provider)
        {
            _provider = provider;
        }

        public void Tett()
        {
            var toggle = new MyFeatToggle(_provider);
            toggle.IsEnable("myuser");
        }
    }
    public class MyFeatToggle : FeatureToggleWithUserRestriction
    {
        public MyFeatToggle(IFeatureToggleProvider provider) : base(provider)
        {

        }
    }

}
