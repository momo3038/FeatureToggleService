using System;
using System.Collections.Generic;

namespace FeatureToggleService.Data
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
