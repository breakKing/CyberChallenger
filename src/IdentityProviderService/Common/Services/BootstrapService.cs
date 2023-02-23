using IdentityProviderService.Common.Constants;
using IdentityProviderService.Common.Interfaces;
using IdentityProviderService.Persistence;
using IdentityProviderService.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;

namespace IdentityProviderService.Common.Services;

public sealed class BootstrapService : IBootstrapService
{
    private readonly IOpenIddictApplicationManager _applicationManager;
    private readonly ILogger<BootstrapService> _logger;
    private readonly IdentityContext _identityContext;
    private readonly IWebHostEnvironment _environment;
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    private const string OpenIdClientId = "cyber_challenger";
    private const string OpenIdClientSecret = "cyber_challenger_secret";
    private const string OpenIdClientName = "CyberChallenger";
    
    private const string SuperAdminUserName = "superadmin";
    private const string SuperAdminPassword = "6gWGFRN6L8RRMnvz";
    
    public BootstrapService(IServiceScopeFactory serviceScopeFactory)
    {
        var scope = serviceScopeFactory.CreateScope();

        _applicationManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
        _logger = scope.ServiceProvider.GetRequiredService<ILogger<BootstrapService>>();
        _identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
        _environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
        _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        _userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    }

    /// <inheritdoc />
    public async Task BootstrapAsync()
    {
        _logger.LogInformation("Starting bootstrap process...");
        await MigrateDevelopmentDatabaseAsync();
        await CreateApplicationIfNeededAsync();
        await CreateRolesIfNeededAsync();
        await CreateSuperAdminIfNeededAsync();
        _logger.LogInformation("Bootstrap process completed successfully");
    }

    private async Task MigrateDevelopmentDatabaseAsync()
    {
        if (!_environment.IsDevelopment())
        {
            return;
        }
        
        _logger.LogInformation("Migrating database...");
        await _identityContext.Database.MigrateAsync();
        _logger.LogInformation("Database migrated successfully");
    }

    private async Task CreateRolesIfNeededAsync()
    {
        _logger.LogInformation("Creating roles...");
        
        // Выгружаем роли, которые должны быть в системе согласно InitialData
        var roleIds = await _roleManager.Roles
            .Where(r => InitialData.Roles
                .Select(ir => ir.Id)
                .Contains(r.Id))
            .Select(r => r.Id)
            .ToListAsync();

        var rolesToCreate = InitialData.Roles
            .ExceptBy(roleIds, r => r.Id)
            .ToList();

        foreach (var role in rolesToCreate)
        {
            await _roleManager.CreateAsync(role);
        }
        
        _logger.LogInformation("Roles have been successfully created");
    }

    private async Task CreateSuperAdminIfNeededAsync()
    {
        _logger.LogInformation("Checking if a superadmin user should be created...");
        
        var user = await _userManager.FindByNameAsync(SuperAdminUserName);
        if (user is not null)
        {
            _logger.LogInformation("Superadmin user exists, ensuring it is in a role...");
            await _userManager.AddToRoleAsync(user, RoleNames.SuperAdmin);
            _logger.LogInformation("Superadmin is in a role, continuing...");
            
            return;
        }
        
        _logger.LogInformation("Superadmin user does not exists, creating...");

        user = new User(Guid.Parse("1290f74e-f3a3-43c6-b994-67b5f7c392e4"), SuperAdminUserName);
        user.Email = "dduhovny26@gmail.com";
        user.EmailConfirmed = true;
        
        var role = await _roleManager.FindByNameAsync(RoleNames.SuperAdmin);
        if (role is null)
        {
            _logger.LogWarning("Superadmin role does not exist. Aborting superadmin creation...");
            return;
        }
        user.Roles = new List<Role> { role };

        await _userManager.CreateAsync(user, SuperAdminPassword);
        
        _logger.LogInformation("Superadmin user has been successfully created");
    }

    private async Task CreateApplicationIfNeededAsync()
    {
        _logger.LogInformation("Checking if an open id app should be created...");

        if (await _applicationManager.FindByClientIdAsync(OpenIdClientId) is not null)
        {
            _logger.LogInformation("Open id app exists, continuing...");
            return;
        }
        
        _logger.LogInformation("Open id app does not exists, creating...");

        var appDescriptor = new OpenIddictApplicationDescriptor
        {
            ClientId = OpenIdClientId,
            ClientSecret = OpenIdClientSecret,
            DisplayName = OpenIdClientName,
            Permissions =
            {
                OpenIddictConstants.Permissions.Endpoints.Token,
                OpenIddictConstants.Permissions.Endpoints.Revocation,
                OpenIddictConstants.Permissions.Endpoints.Introspection,
                
                OpenIddictConstants.Permissions.GrantTypes.Password,
                OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                
                OpenIddictConstants.Permissions.Scopes.Profile,
                OpenIddictConstants.Permissions.Scopes.Roles
            }
        };

        await _applicationManager.CreateAsync(appDescriptor);
        
        _logger.LogInformation("Open id app has been successfully created");
    }
}