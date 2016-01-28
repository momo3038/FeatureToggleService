using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
