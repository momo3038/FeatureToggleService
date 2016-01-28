using System.Collections.Generic;
using FeatureToggleService.Data;
using FeatureToggleService.WebApi;
using NFluent;
using NSubstitute;
using NUnit.Framework;

namespace FeatureToggleService.WebApi.Test
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
        public void Should_return_toggle_when_get_all_by_type()
        {
            _featureToogleRepository
                .GetAllByType(Arg.Any<string>())
                .Returns(new List<FeatureToggle> { new FeatureToggle("Test") });

            var result = _controller.GetAllByType("MyTypeOfToggle");

            _featureToogleRepository
                .Received(1)
                .GetAllByType(Arg.Is("MyTypeOfToggle"));

            Check.That(result).IsNotNull();
            Check.That(result.Count()).IsEqualTo(1);
        }

        [Test]
        public void Should_return_empty_list_if_no_type_provided()
        {
            var result = _controller.GetAllByType(null);

            Check.That(result).IsNotNull();
            Check.That(result.Count()).IsEqualTo(0);

            result = _controller.GetAllByType("");

            Check.That(result).IsNotNull();
            Check.That(result.Count()).IsEqualTo(0);

            _featureToogleRepository
                .Received(2)
                .GetAllByType(Arg.Any<string>());
        }
    }
}