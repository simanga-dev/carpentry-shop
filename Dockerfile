FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source
COPY CarpentryShop.csproj ./
COPY CarpentryShop.ServiceDefaults/CarpentryShop.ServiceDefaults.csproj CarpentryShop.ServiceDefaults/
RUN dotnet restore CarpentryShop.csproj
COPY . .
RUN dotnet publish CarpentryShop.csproj --configuration Release --no-restore -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
USER root
RUN apt-get update && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/* \
    && mkdir -p /home/app/.aspnet/DataProtection-Keys \
    && chown -R app:app /home/app/.aspnet
COPY --from=build /app/publish .
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
USER $APP_UID
ENTRYPOINT ["dotnet", "CarpentryShop.dll"]
