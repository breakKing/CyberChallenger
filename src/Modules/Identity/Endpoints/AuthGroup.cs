using Common.Presentation.Abstractions;
using FastEndpoints;

namespace Identity.Endpoints;

public sealed class AuthGroup : EndpointGroupBase
{
    public AuthGroup() : base("Auth", "auth")
    {
        
    }
}