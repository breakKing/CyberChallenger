using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shared.Infrastructure.EventSourcing.Configurations;

namespace Shared.Infrastructure.EventSourcing.Extensions;

public static class DbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder AddEntitiesForEventSourcing(this DbContextOptionsBuilder builder)
    {
        return builder.ReplaceService<IModelCustomizer, EventSourcingModelCustomizer>();
    }
}