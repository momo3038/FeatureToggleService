using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
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
        public void CanRetrieveFeatureToggleByType()
        {
            _dbConnection.Query<ToggleFeat>(Arg.Any<string>()).Returns(new List<ToggleFeat>(){ new ToggleFeat()});
            var repository = new FeatureToggleRepository(_dbConnection);

            var result = repository.GetAllByType("MyType");

            Check.That(result).IsNotNull();
            Check.That(result.Count).IsEqualTo(1);
        }
    }
}
