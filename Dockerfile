FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
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
ARG VERSION=1.0.0
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
RUN if [ "$VERSION" ]; \
    then dotnet build "Console.csproj" -c $BUILD_CONFIGURATION -o /app/build  /p:Version=$VERSION /p:AssemblyVersion=$VERSION /p:FileVersion=$VERSION; \
    else dotnet build "Console.csproj" -c $BUILD_CONFIGURATION -o /app/build; \
    fi

COPY ["./WebAPI", "/src/WebAPI"]
WORKDIR /src/WebAPI
RUN dotnet restore
RUN if [ "$VERSION" ]; \
    then dotnet build "WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build /p:Version=$VERSION /p:AssemblyVersion=$VERSION /p:FileVersion=$VERSION; \
    else dotnet build "WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build; \
    fi


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/Console
RUN dotnet publish "Console.csproj" -c $BUILD_CONFIGURATION -o /app/publish

WORKDIR /src/WebAPI
RUN dotnet publish "WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
RUN dotnet tool install -g dotnet-ef
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet ef migrations bundle
RUN mv /src/WebAPI/efbundle /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ./console.sh /app/console
COPY ./console-fr.sh /app/console-fr
COPY ./entrypoint.sh /app/entrypoint.sh
USER root
RUN chown webapp:webapp /app/console
RUN chown webapp:webapp /app/console-fr
RUN chmod +x /app/console
RUN chmod +x /app/console-fr
USER webapp
RUN echo "export PATH=/app:${PATH}" >> /home/webapp/.bashrc
ENV PATH=$PATH:/app
ENTRYPOINT ["/bin/bash", "/app/entrypoint.sh"]
