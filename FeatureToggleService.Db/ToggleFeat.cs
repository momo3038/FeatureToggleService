using System;

namespace FeatureToggleService.Db
{
    public class ToggleFeat
    {
        public ToggleFeat()
        {
            
        }

        public ToggleFeat(string name, string type = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Type = type;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Enable { get; set; }
        public string Description { get; set; }
    }
}