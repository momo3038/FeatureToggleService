using System.Collections.Generic;

namespace FeatureToggleService.Data
{
    public class FeatureToggleByType
    {
        public string Type { get; set; }
        public IList<FeatureToggle> Features { get; set; }
    }
}