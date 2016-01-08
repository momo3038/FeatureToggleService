using System.Collections.Generic;
using FeatureToggleService.Db;
using FeatureToggleService.WebApi;
using NFluent;
using NSubstitute;
using NUnit.Framework;

namespace FeatureToggle.WebApi.Test
{
    public class FeatureToggleControllerTest
    {
        private IFeatureToggleRepository _featureToogleRepository;
        private FeatureToggleController _controller;

        [SetUp]
        public void Init()
        {
            _featureToogleRepository = Substitute.For<IFeatureToggleRepository>();
            _controller = new FeatureToggleController(_featureToogleRepository);
        }

        [Test]
        public void ShouldCallFeatureRepositoryWhenGetAllForType()
        {
            _featureToogleRepository
                .GetAllByType(Arg.Any<string>())
                .Returns(new List<ToggleFeat> {new ToggleFeat()});

            var result = _controller.GetAllByType("MyTypeOfToggle");

            _featureToogleRepository
                .Received(1)
                .GetAllByType(Arg.Is("MyTypeOfToggle"));

            Check.That(result).IsNotNull();
            Check.That(result.Count()).IsEqualTo(1);
        }
    }
}