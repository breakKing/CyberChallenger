FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/TeamService/TeamService.csproj", "src/TeamService/"]
RUN dotnet restore "src/TeamService/TeamService.csproj"
COPY . .
WORKDIR "/src/src/TeamService"
RUN dotnet build "TeamService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TeamService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeamService.dll"]
