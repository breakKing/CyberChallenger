FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/GatewayApi/GatewayApi.csproj", "src/GatewayApi/"]
RUN dotnet restore "src/GatewayApi/GatewayApi.csproj"
COPY . .
WORKDIR "/src/src/GatewayApi"
RUN dotnet build "GatewayApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GatewayApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GatewayApi.dll"]
