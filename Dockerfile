#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ARG NUGET_SOURCE_PWD

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["MenuPlanner/Server/MenuPlanner.Server.csproj", "MenuPlanner/Server/"]
COPY ["MenuPlanner/Client/MenuPlanner.Client.csproj", "MenuPlanner/Client/"]
COPY ["MenuPlanner/Shared/MenuPlanner.Shared.csproj", "MenuPlanner/Shared/"]
ARG PAT=githubNuget
COPY nuget.config ./nuget.config
RUN c:\nuget.exe source update -ConfigFile ./nuget.config -Name githubg -username dandevaud -StorePasswordInClearText -password $env:NUGET_SOURCE_PWD
RUN dotnet restore "MenuPlanner/Server/MenuPlanner.Server.csproj"
COPY . .
WORKDIR "/src/MenuPlanner/Server"
RUN dotnet build "MenuPlanner.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MenuPlanner.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MenuPlanner.Server.dll"]
