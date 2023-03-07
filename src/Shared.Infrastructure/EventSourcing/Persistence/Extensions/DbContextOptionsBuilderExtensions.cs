using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shared.Infrastructure.EventSourcing.Persistence.Configurations;

namespace Shared.Infrastructure.EventSourcing.Persistence.Extensions;

public static class DbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder AddEntitiesForEventSourcing(this DbContextOptionsBuilder builder)
    {
        return builder.ReplaceService<IModelCustomizer, EventSourcingModelCustomizer>();
    }
}