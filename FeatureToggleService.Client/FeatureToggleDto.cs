using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FeatureToggleService.Client
{
    [DataContract]
    public class FeatureToggleDto
    {
        [DataMember]
        public bool IsEnable { get; set; }
        [DataMember]
        public string Name { get; set; }
        public List<string> Users { get; set; }
    }
}
