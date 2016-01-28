using System;
using System.Collections.Generic;
using DapperWrapper;
using FeatureToggleService.Db;
using NFluent;
using NSubstitute;
using NUnit.Framework;

namespace FeatureToggle.Db
{
    public class SqlServerRepositoryTest
    {
        private IDbExecutor _dbConnection;

        [SetUp]
        public void Setup()
        {
            _dbConnection = Substitute.For<IDbExecutor>();
        }

        [Test]
        public void Should_retrieve_all_toggle()
        {
            _dbConnection.Query<FeatureToggleService.Db.FeatureToggle>(Arg.Any<string>()).Returns(new List<FeatureToggleService.Db.FeatureToggle>() { new FeatureToggleService.Db.FeatureToggle("Test") });
            var repository = new SqlServerRepository(_dbConnection);

            var result = repository.GetAll();

            Check.That(result).IsNotNull();
            Check.That(result.Count).IsEqualTo(1);
        }

        [Test]
        public void Should_retrieve_a_feature_toggle_by_type()
        {
            _dbConnection.Query<FeatureToggleService.Db.FeatureToggle>(Arg.Any<string>(), Arg.Any<object>()).Returns(new List<FeatureToggleService.Db.FeatureToggle>() { new FeatureToggleService.Db.FeatureToggle("Test", "MyType") });
            var repository = new SqlServerRepository(_dbConnection);

            var result = repository.GetAllByType("MyType");

            Check.That(result).IsNotNull();
            Check.That(result.Count).IsEqualTo(1);
        }

        [Test]
        public void Should_change_a_feature_value_and_audit()
        {
            var featureToDelete = new FeatureToggleService.Db.FeatureToggle("MyFeatureToDelete");

            var repository = new SqlServerRepository(_dbConnection);

            repository.ChangeValue(featureToDelete.Id, true);

            _dbConnection.Received(1).Execute("update FeatureToggle set Enable = @Value where Id = @Id", Arg.Any<object>());
            _dbConnection.Received(1).Execute("insert into FeatureToggleAudit (Id, Status, Enable, ModificationDate) values (@Id, @Status, @Enable, @ModificationDate)", Arg.Any<object>());
        }

        [Test]
        public void Should_create_a_feature_and_audit()
        {
            var featureToCreate = new FeatureToggleService.Db.FeatureToggle("MyFeatureToDelete")
            {
                Type = "Test",
                Enable = true
            };

            var repository = new SqlServerRepository(_dbConnection);

            repository.Create(featureToCreate);

            _dbConnection.Received(1).Execute("insert into FeatureToggle (Id, Name, Type, Enable, Description) values (@Id, @Name, @Type, @Enable, @Description)", Arg.Any<object>());
            _dbConnection.Received(1).Execute("insert into FeatureToggleAudit (Id, Status, Enable, ModificationDate) values (@Id, @Status, @Enable, @ModificationDate)", Arg.Any<object>());
        }

        [Test]
        public void Should_delete_a_feature_and_audit()
        {
            var featureToDelete = new FeatureToggleService.Db.FeatureToggle("MyFeatureToDelete");

            var repository = new SqlServerRepository(_dbConnection);

            repository.Delete(featureToDelete.Id);

            _dbConnection.Received(1).Execute("delete FeatureToggle where Id = @Id", Arg.Any<object>());
            _dbConnection.Received(1).Execute("insert into FeatureToggleAudit (Id, Status, Enable, ModificationDate) values (@Id, @Status, @Enable, @ModificationDate)", Arg.Any<object>());
        }
    }
}
