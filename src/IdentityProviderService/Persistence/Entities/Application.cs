using MassTransit;
using OpenIddict.EntityFrameworkCore.Models;

namespace IdentityProviderService.Persistence.Entities;

public sealed class Application : OpenIddictEntityFrameworkCoreApplication<Guid, Authorization, Token>
{
    /// <inheritdoc />
    public Application()
    {
        Id = NewId.NextGuid();
    }
}