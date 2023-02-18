using IdentityProviderService.Common.Interfaces;
using OpenIddict.Abstractions;

namespace IdentityProviderService.Common.Services;

public sealed class BootstrapService : IBootstrapService
{
    private readonly IOpenIddictApplicationManager _applicationManager;
    private readonly ILogger<BootstrapService> _logger;

    private const string OpenIdClientId = "cyber_challenger";
    private const string OpenIdClientName = "CyberChallenger";
    
    public BootstrapService(IServiceScopeFactory serviceScopeFactory)
    {
        var scope = serviceScopeFactory.CreateScope();

        _applicationManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
        _logger = scope.ServiceProvider.GetRequiredService<ILogger<BootstrapService>>();
    }

    /// <inheritdoc />
    public async Task BootstrapAsync()
    {
        _logger.LogInformation("Starting bootstrap process...");
        await CreateApplicationIfNeededAsync();
        _logger.LogInformation("Bootstrap process completed successfully");
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
            DisplayName = OpenIdClientName,
            Permissions =
            {
                OpenIddictConstants.Permissions.Endpoints.Token,
                OpenIddictConstants.Permissions.Endpoints.Logout,
                
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