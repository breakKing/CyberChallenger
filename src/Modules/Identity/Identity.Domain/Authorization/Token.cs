using MassTransit;
using OpenIddict.EntityFrameworkCore.Models;

namespace Identity.Domain.Authorization;

public sealed class Token : OpenIddictEntityFrameworkCoreToken<Guid, Application, Authorization>
{
    /// <inheritdoc />
    public Token()
    {
        Id = NewId.NextGuid();
    }
}