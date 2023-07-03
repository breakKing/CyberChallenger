using IdentityProvider.Common.Constants;
using IdentityProvider.Persistence.Entities;

namespace IdentityProvider.Persistence;

public static class InitialData
{
    public static Role[] Roles = 
    {
        new(Guid.Parse("230815e7-f3cb-4689-b491-d032db8b906e"), RoleNames.SuperAdmin),
        new(Guid.Parse("2a9789fa-9302-4195-8bed-93ccb7003a80"), RoleNames.Admin),
        new(Guid.Parse("25caf262-5c9c-446d-8548-0bdf508545ed"), RoleNames.TournamentOperator),
        new(Guid.Parse("3e21e164-5138-4c12-bf61-836f80017e96"), RoleNames.Player),
        new(Guid.Parse("4e7ad16d-27b2-40b0-ae56-0e4419b9837c"), RoleNames.Common)
    };
}