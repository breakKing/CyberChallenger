using MassTransit;
using OpenIddict.EntityFrameworkCore.Models;

namespace IdentityProviderService.Persistence.Entities;

public sealed class Token : OpenIddictEntityFrameworkCoreToken<Guid, Application, Authorization>
{
    /// <inheritdoc />
    public Token()
    {
        Id = NewId.NextGuid();
    }
}