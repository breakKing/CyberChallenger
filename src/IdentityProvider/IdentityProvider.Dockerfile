FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/IdentityProviderService/IdentityProviderService.csproj", "src/IdentityProviderService/"]
RUN dotnet restore "src/IdentityProviderService/IdentityProviderService.csproj"
COPY . .
WORKDIR "/src/src/IdentityProviderService"
RUN dotnet build "IdentityProviderService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityProviderService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityProviderService.dll"]
