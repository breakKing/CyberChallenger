using IdentityProviderService.Persistence;
using IdentityProviderService.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IdentityProviderService.Common.Services;

public sealed class CustomRoleStore : RoleStore<Role, IdentityContext, Guid>
{
    /// <inheritdoc />
    public CustomRoleStore(IdentityContext context, IdentityErrorDescriber? describer = null) : 
        base(context, describer)
    {
    }

    /// <inheritdoc />
    public override async Task<IdentityResult> DeleteAsync(Role role, 
        CancellationToken ct = new CancellationToken())
    {
        role.IsDeleted = true;
        return await UpdateAsync(role, ct);
    }
}