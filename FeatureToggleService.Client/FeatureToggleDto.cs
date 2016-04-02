using System.Collections.Generic;

namespace FeatureToggleService.Client
{
    public class FeatureToggleDto
    {
        public bool IsEnable { get; set; }
        public string Name { get; set; }
        public List<string> Users { get; set; }
    }
}
