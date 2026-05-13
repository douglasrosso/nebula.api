using Microsoft.EntityFrameworkCore;
using nebula.api.src.Data;

namespace nebula.api.tests.Unit.Helpers;

internal static class DbContextFactory
{
    internal static NebulaDbContext Create()
    {
        var options = new DbContextOptionsBuilder<NebulaDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new NebulaDbContext(options);
    }
}
