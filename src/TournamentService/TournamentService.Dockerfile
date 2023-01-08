﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/TournamentService/TournamentService.csproj", "src/TournamentService/"]
RUN dotnet restore "src/TournamentService/TournamentService.csproj"
COPY . .
WORKDIR "/src/src/TournamentService"
RUN dotnet build "TournamentService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TournamentService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TournamentService.dll"]
