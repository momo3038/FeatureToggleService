using System.Collections.Generic;
using FeatureToggleService.Db;
using FeatureToggleService.WebApi;
using NFluent;
using NSubstitute;
using NUnit.Framework;

namespace FeatureToggle.WebApi.Test
{
    public class AdminControllerTest
    {
        private IFeatureToggleRepository _featureToogleRepository;
        private AdminController _controller;

        [SetUp]
        public void Init()
        {
            _featureToogleRepository = Substitute.For<IFeatureToggleRepository>();
            _controller = new AdminController(_featureToogleRepository);
        }

        [Test]
        public void ShouldGetAllFeatureWhenGetAllRoute()
        {
            _featureToogleRepository
                .GetAll()
                .Returns(new List<ToggleFeat> {new ToggleFeat("Test")});

            var result = _controller.GetAll();

            _featureToogleRepository
                .Received(1)
                .GetAll();

            Check.That(result).IsNotNull();
            Check.That(result.Count()).IsEqualTo(1);
        }

        [Test]
        public void ShouldCreateAFeatureWhenCreateRoute()
        {
            var featureToCreate = new ToggleFeat("MyFeatureToDelete")
            {
                Type = "Test",
                Enable = true
            };

            _controller.Create(featureToCreate);

            _featureToogleRepository
                .Received(1)
                .Create(Arg.Is(featureToCreate));
        }

        [Test]
        public void ShouldChangeFeatureValueWhenChangeRoute()
        {
            var featureToDelete = new ToggleFeat("MyFeatureToModify");

            _controller.ChangeValue(featureToDelete.Id, true);

            _featureToogleRepository
                .Received(1)
                .ChangeValue(Arg.Is(featureToDelete.Id), Arg.Is(true));
        }

        [Test]
        public void ShouldDeleteAFeatureWhenDeleteRoute()
        {
            var featureToDelete = new ToggleFeat("MyFeatureToDelete");

            _controller.Delete(featureToDelete.Id);

            _featureToogleRepository
                .Received(1)
                .Delete(Arg.Is(featureToDelete.Id));
        }
    }
}