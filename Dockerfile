﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ARG APP_UID

USER root

RUN groupadd -g $APP_UID webapp
RUN useradd -m -u $APP_UID -g $APP_UID -s /bin/bash webapp

USER webapp
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["./Domain", "/src/Domain"]
WORKDIR /src/Domain
RUN dotnet restore

COPY ["./Application", "/src/Application"]
WORKDIR /src/Application
RUN dotnet restore

COPY ["./Infrastructure", "/src/Infrastructure"]
WORKDIR /src/Infrastructure
RUN dotnet restore

COPY ["./Console", "/src/Console"]
WORKDIR /src/Console
RUN dotnet restore
RUN dotnet build "Console.csproj" -c $BUILD_CONFIGURATION -o /app/build

COPY ["./WebAPI", "/src/WebAPI"]
WORKDIR /src/WebAPI
RUN dotnet restore
RUN dotnet build "WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/Console
RUN dotnet publish "Console.csproj" -c $BUILD_CONFIGURATION -o /app/publish

WORKDIR /src/WebAPI
RUN dotnet publish "WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]
