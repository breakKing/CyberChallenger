using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Common.Presentation.Abstractions;

public abstract class EndpointGroupBase : Group
{
    protected EndpointGroupBase(string groupName, string routePrefix)
    {
        ConfigureBasicParams(groupName, routePrefix);
    }

    private void ConfigureBasicParams(string groupName, string routePrefix)
    {
        Configure(routePrefix, ep =>
        {
            ep.Tags(groupName);
            ep.Options(o => o
                .WithGroupName(groupName)
                .WithTags(groupName));
        });
    }
}