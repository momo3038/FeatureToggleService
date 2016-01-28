using System;
using System.Collections.Generic;
using DapperWrapper;

namespace FeatureToggleService.Db
{
    public interface IFeatureToggleRepository
    {
        IList<FeatureToggle> GetAllByType(string type);
        IList<FeatureToggle> GetAll();
        void Delete(Guid id);
        void ChangeValue(Guid featureId, bool value);
        void Create(FeatureToggle feature);
    }

    public class FeatureToggleRepository : IFeatureToggleRepository
    {
        private readonly IDbExecutor _connection;

        public FeatureToggleRepository(IDbExecutor connection)
        {
            _connection = connection;
        }

        public IList<FeatureToggle> GetAllByType(string type)
        {
            var toggleFeats = _connection
                .Query<FeatureToggle>("select * from FeatureToggle where Type = @featType", new { featType = type });

            return (IList<FeatureToggle>)toggleFeats;
        }

        public IList<FeatureToggle> GetAll()
        {
            var toggleFeats = _connection.Query<FeatureToggle>("select * from FeatureToggle");
            return (IList<FeatureToggle>)toggleFeats;
        }

        public void Delete(Guid id)
        {
            _connection.Execute("delete FeatureToggle where Id = @Id", new { Id = id });
            AddAudit(id, FeatureStatus.Deleted);
        }

        public void ChangeValue(Guid featureId, bool value)
        {
            _connection.Execute("update FeatureToggle set Enable = @Value where Id = @Id", new { Value = value, Id = featureId });
            AddAudit(featureId, FeatureStatus.Modified, value);
        }

        public void Create(FeatureToggle feature)
        {
            _connection.Execute("insert into FeatureToggle (Id, Name, Type, Enable, Description) values (@Id, @Name, @Type, @Enable, @Description)",
                new
                {
                    Id = feature.Id,
                    Name = feature.Name,
                    Type = feature.Type,
                    Enable = feature.Enable,
                    Description = feature.Description
                });

            AddAudit(feature.Id, FeatureStatus.Created, feature.Enable);
        }

        private void AddAudit(Guid featureId, FeatureStatus status, bool? value = null)
        {
            _connection.Execute(
                "insert into FeatureToggleAudit (Id, Status, Enable, ModificationDate) values (@Id, @Status, @Enable, @ModificationDate)", new
                {
                    Id = featureId,
                    Status = status,
                    Enable = value,
                    ModificationDate = DateTime.Now
                });
        }
    }

    internal enum FeatureStatus
    {
        Created,
        Modified,
        Deleted
    }
}
