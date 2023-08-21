# Learn about building .NET container images:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.csproj .
# RUN dotnet restore --use-current-runtime

# copy and publish app and libraries
COPY . .
RUN dotnet build --configuration Release --disable-parallel


# final stage/image
FROM mcr.microsoft.com/dotnet/runtime:7.0
WORKDIR /app
COPY --from=build  source/bin/Release/net6.0 .
ENTRYPOINT ["dotnet", "CarpentryShop.dll"]