using System;
using System.Collections.Generic;
using DapperWrapper;
using FeatureToggleService.Db;
using NFluent;
using NSubstitute;
using NUnit.Framework;

namespace FeatureToggle.Db
{
    public class FeatureToggleRepositoryTest
    {
        private IDbExecutor _dbConnection;

        [SetUp]
        public void Setup()
        {
            _dbConnection = Substitute.For<IDbExecutor>();
        }

        [Test]
        public void CanRetrieveAllToggle()
        {
            _dbConnection.Query<ToggleFeat>(Arg.Any<string>()).Returns(new List<ToggleFeat>() { new ToggleFeat("Test") });
            var repository = new FeatureToggleRepository(_dbConnection);

            var result = repository.GetAll();

            Check.That(result).IsNotNull();
            Check.That(result.Count).IsEqualTo(1);
        }

        [Test]
        public void CanRetrieveFeatureToggleByType()
        {
            _dbConnection.Query<ToggleFeat>(Arg.Any<string>(), Arg.Any<object>()).Returns(new List<ToggleFeat>() { new ToggleFeat("Test", "MyType") });
            var repository = new FeatureToggleRepository(_dbConnection);

            var result = repository.GetAllByType("MyType");

            Check.That(result).IsNotNull();
            Check.That(result.Count).IsEqualTo(1);
        }

        [Test]
        public void ShouldChangeAFeatureValueAndAudit()
        {
            var featureToDelete = new ToggleFeat("MyFeatureToDelete");

            var repository = new FeatureToggleRepository(_dbConnection);

            repository.ChangeValue(featureToDelete.Id, true);

            _dbConnection.Received(1).Execute("update FeatureToggle set Enable = @Value where Id = @Id", Arg.Any<object>());
            _dbConnection.Received(1).Execute("insert into FeatureToggleAudit (Id, Status, Enable, @ModificationDate) values (@Id, @Status, @Enable, @ModificationDate)", Arg.Any<object>());
        }

        [Test]
        public void ShouldCreateAFeatureAndAudit()
        {
            var featureToCreate = new ToggleFeat("MyFeatureToDelete")
            {
                Type = "Test",
                Enable = true
            };

            var repository = new FeatureToggleRepository(_dbConnection);

            repository.Create(featureToCreate);

            _dbConnection.Received(1).Execute("insert into FeatureToggle (Id, Name, Type, Enable, Description) values (@Id, @Name, @Type, @Enable, @Description)", Arg.Any<object>());
            _dbConnection.Received(1).Execute("insert into FeatureToggleAudit (Id, Status, Enable, @ModificationDate) values (@Id, @Status, @Enable, @ModificationDate)", Arg.Any<object>());
        }

        [Test]
        public void ShouldDeleteAFeatureAndAudit()
        {
            var featureToDelete = new ToggleFeat("MyFeatureToDelete");

            var repository = new FeatureToggleRepository(_dbConnection);

            repository.Delete(featureToDelete.Id);

            _dbConnection.Received(1).Execute("delete FeatureToggle where Id = @Id", Arg.Any<object>());
            _dbConnection.Received(1).Execute("insert into FeatureToggleAudit (Id, Status, Enable, @ModificationDate) values (@Id, @Status, @Enable, @ModificationDate)", Arg.Any<object>());
        }
    }
}
