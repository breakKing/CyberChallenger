using MassTransit;
using OpenIddict.EntityFrameworkCore.Models;

namespace Identity.Domain.Authorization;

public sealed class Authorization : OpenIddictEntityFrameworkCoreAuthorization<Guid, Application, Token>
{
    /// <inheritdoc />
    public Authorization()
    {
        Id = NewId.NextGuid();
    }
}