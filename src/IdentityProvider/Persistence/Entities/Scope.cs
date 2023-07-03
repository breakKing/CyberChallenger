using MassTransit;
using OpenIddict.EntityFrameworkCore.Models;

namespace IdentityProviderService.Persistence.Entities;

public sealed class Scope : OpenIddictEntityFrameworkCoreScope<Guid>
{
    /// <inheritdoc />
    public Scope()
    {
        Id = NewId.NextGuid();
    }
}