using FeatureToggleService.Client.Provider;
using NFluent;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace FeatureToggleService.Client.Test
{
    public class WithUserRestrictionToggleTest
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
                    IsEnable = true,
                    Users = new List<string>() { "john.doe@test.com", "other.login" }
                });

            var myToggle = new MyToogleWithUser(_provider);
            Check.That(myToggle.IsEnable("john.doe@test.com")).IsTrue();
        }
    }
}
