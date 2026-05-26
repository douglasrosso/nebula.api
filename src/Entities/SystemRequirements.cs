namespace nebula.api.src.Entities
{
    public class SystemRequirements
    {
        public SystemRequirementSpec Minimum { get; set; } = new();
        public SystemRequirementSpec Recommended { get; set; } = new();
    }

    public class SystemRequirementSpec
    {
        public string Os { get; set; } = string.Empty;
        public string Processor { get; set; } = string.Empty;
        public string Memory { get; set; } = string.Empty;
        public string Graphics { get; set; } = string.Empty;
        public string Storage { get; set; } = string.Empty;
    }
}
