using System;
using System.Collections.Generic;
using DapperWrapper;

namespace FeatureToggleService.Db
{
    public interface IFeatureToggleRepository
    {
        IList<ToggleFeat> GetAllByType(string type);
        IList<ToggleFeat> GetAll();
        void Delete(Guid id);
        void ChangeValue(Guid featureId, bool value);
        void Create(ToggleFeat feature);
    }

    public class FeatureToggleRepository : IFeatureToggleRepository
    {
        private readonly IDbExecutor _connection;

        public FeatureToggleRepository(IDbExecutor connection)
        {
            _connection = connection;
        }

        public IList<ToggleFeat> GetAllByType(string type)
        {
            var toggleFeats = _connection
                .Query<ToggleFeat>("select * from FeatureToggle where Type = @featType", new { featType = type });

            return (IList<ToggleFeat>)toggleFeats;
        }

        public IList<ToggleFeat> GetAll()
        {
            var toggleFeats = _connection.Query<ToggleFeat>("select * from FeatureToggle");
            return (IList<ToggleFeat>)toggleFeats;
        }

        public void Delete(Guid id)
        {
            _connection.Execute("delete FeatureToggle where Id = @Id", new { Id = id });
        }

        public void ChangeValue(Guid featureId, bool value)
        {
            _connection.Execute("update FeatureToggle set Enable = @Value where Id = @Id", new { Value = value, Id = featureId });
        }

        public void Create(ToggleFeat feature)
        {
            _connection.Execute("insert into FeatureToggle set Id = @Id, Name = @Name, Type = @Type, Enable = @Enable, Description = @Description",
                new
                {
                    Id = feature.Id,
                    Name = feature.Name,
                    Type = feature.Type,
                    Enable = feature.Enable,
                    Description = feature.Description
                });
        }
    }
}
