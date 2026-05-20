using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using nebula.api.src.Mappings;

namespace nebula.api.tests.Unit.Mappings;

public class MappingProfileTests
{
    [Fact]
    public void AutoMapper_configuration_is_valid()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<EntityToModelProfile>();
            cfg.AddProfile<ModelToDtoProfile>();
        }, NullLoggerFactory.Instance);

        config.AssertConfigurationIsValid();
    }
}
