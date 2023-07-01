using MassTransit;
using OpenIddict.EntityFrameworkCore.Models;

namespace Identity.Domain.Authorization;

public sealed class Application : OpenIddictEntityFrameworkCoreApplication<Guid, Authorization, Token>
{
    /// <inheritdoc />
    public Application()
    {
        Id = NewId.NextGuid();
    }
}