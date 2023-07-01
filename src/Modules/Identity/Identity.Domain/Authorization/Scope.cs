using MassTransit;
using OpenIddict.EntityFrameworkCore.Models;

namespace Identity.Domain.Authorization;

public sealed class Scope : OpenIddictEntityFrameworkCoreScope<Guid>
{
    /// <inheritdoc />
    public Scope()
    {
        Id = NewId.NextGuid();
    }
}