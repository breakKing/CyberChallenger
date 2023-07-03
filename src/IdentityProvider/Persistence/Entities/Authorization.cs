using MassTransit;
using OpenIddict.EntityFrameworkCore.Models;

namespace IdentityProvider.Persistence.Entities;

public sealed class Authorization : OpenIddictEntityFrameworkCoreAuthorization<Guid, Application, Token>
{
    /// <inheritdoc />
    public Authorization()
    {
        Id = NewId.NextGuid();
    }
}