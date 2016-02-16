using System.Collections.Generic;
using FeatureToggleService.Data;
using NFluent;
using NSubstitute;
using NUnit.Framework;

namespace FeatureToggleService.WebApi.Test
{
    public class AdminControllerTest
    {
        private IFeatureToggleRepository _featureToogleRepository;
        private IFeatureToggleValidator _validator;
        private AdminController _controller;

        [SetUp]
        public void Init()
        {
            _featureToogleRepository = Substitute.For<IFeatureToggleRepository>();
            _validator = Substitute.For<IFeatureToggleValidator>();
            _controller = new AdminController(_featureToogleRepository, _validator);
        }

        [Test]
        public void Should_get_all_feature_whenGetAllRoute()
        {
            _featureToogleRepository
                .GetAll()
                .Returns(new List<FeatureToggle> {new FeatureToggle("Test")});

            var result = _controller.GetAll();

            _featureToogleRepository
                .Received(1)
                .GetAll();

            Check.That(result).IsNotNull();
            Check.That(result.Count()).IsEqualTo(1);
        }

        [Test]
        public void Should_create_a_feature_when_asked_for()
        {
            var featureToCreate = new FeatureToggle("MyFeatureToDelete")
            {
                Type = "Test",
                Enable = true
            };

            _controller.Create(featureToCreate);

            _validator
                .Received(1)
                .ThrowExceptionIfError(Arg.Is(featureToCreate));

            _featureToogleRepository
                .Received(1)
                .Create(Arg.Is(featureToCreate));
        }

        [Test]
        public void Should_change_feature_value_when_asked_for()
        {
            var featureToDelete = new FeatureToggle("MyFeatureToModify");

            _controller.ChangeValue(featureToDelete.Id, true);

            _featureToogleRepository
                .Received(1)
                .ChangeValue(Arg.Is(featureToDelete.Id), Arg.Is(true));
        }

        [Test]
        public void Should_delete_a_feature_when_asked_for()
        {
            var featureToDelete = new FeatureToggle("MyFeatureToDelete");

            _controller.Delete(featureToDelete.Id);

            _featureToogleRepository
                .Received(1)
                .Delete(Arg.Is(featureToDelete.Id));
        }
    }
}