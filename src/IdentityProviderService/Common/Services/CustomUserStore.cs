using IdentityProviderService.Persistence;
using IdentityProviderService.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IdentityProviderService.Common.Services;

public sealed class CustomUserStore : UserStore<User, Role, IdentityContext, Guid>
{
    /// <inheritdoc />
    public CustomUserStore(IdentityContext context, IdentityErrorDescriber? describer = null) : 
        base(context, describer)
    {
    }

    /// <inheritdoc />
    public override async Task<IdentityResult> DeleteAsync(User user, 
        CancellationToken ct = new CancellationToken())
    {
        user.IsDeleted = true;
        return await UpdateAsync(user, ct);
    }
}