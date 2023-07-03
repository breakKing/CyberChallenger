using MassTransit;
using OpenIddict.EntityFrameworkCore.Models;

namespace IdentityProvider.Persistence.Entities;

public sealed class Scope : OpenIddictEntityFrameworkCoreScope<Guid>
{
    /// <inheritdoc />
    public Scope()
    {
        Id = NewId.NextGuid();
    }
}