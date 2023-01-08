FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/UserProfileService/UserProfileService.csproj", "src/UserProfileService/"]
RUN dotnet restore "src/UserProfileService/UserProfileService.csproj"
COPY . .
WORKDIR "/src/src/UserProfileService"
RUN dotnet build "UserProfileService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserProfileService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserProfileService.dll"]
