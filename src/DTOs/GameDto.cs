namespace nebula.api.src.DTOs
{
    public class GameSummaryDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? OriginalPrice { get; set; }
        public int? Discount { get; set; }
        public string CoverImage { get; set; } = string.Empty;
        public string Developer { get; set; } = string.Empty;
        public string[] Genres { get; set; } = [];
        public decimal Rating { get; set; }
        public int PositivePercentage { get; set; }
    }

    public class GameDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? OriginalPrice { get; set; }
        public int? Discount { get; set; }
        public string CoverImage { get; set; } = string.Empty;
        public string[] Screenshots { get; set; } = [];
        public string Developer { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string ReleaseDate { get; set; } = string.Empty;
        public string[] Genres { get; set; } = [];
        public string[] Tags { get; set; } = [];
        public decimal Rating { get; set; }
        public int ReviewCount { get; set; }
        public int PositivePercentage { get; set; }
        public string[] Features { get; set; } = [];
        public SystemRequirementsDto SystemRequirements { get; set; } = new();
    }

    public class SystemRequirementsDto
    {
        public SystemRequirementSpecDto Minimum { get; set; } = new();
        public SystemRequirementSpecDto Recommended { get; set; } = new();
    }

    public class SystemRequirementSpecDto
    {
        public string Os { get; set; } = string.Empty;
        public string Processor { get; set; } = string.Empty;
        public string Memory { get; set; } = string.Empty;
        public string Graphics { get; set; } = string.Empty;
        public string Storage { get; set; } = string.Empty;
    }
}
