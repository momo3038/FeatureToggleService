using System.Collections.Generic;
using DapperWrapper;

namespace FeatureToggleService.Db
{
    public interface IFeatureToggleRepository
    {
        IList<ToggleFeat> GetAllByType(string type);
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
            var query = string.Format("select * from FeatureToggle where Type = '{0}'", type);
            var toggleFeats = _connection.Query<ToggleFeat>(query);
            return (IList<ToggleFeat>) toggleFeats;
        }
    }
}
