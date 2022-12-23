FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0.302 AS build
WORKDIR /build
COPY ./Demo.ProductCatalog.Api/Demo.ProductCatalog.Api.csproj ./Demo.ProductCatalog.Api/Demo.ProductCatalog.Api.csproj
COPY ./Demo.SharedModel/Demo.SharedModel.csproj ./Demo.SharedModel/Demo.SharedModel.csproj
COPY ./Demo.OpenTelemetry/Demo.OpenTelemetry.csproj ./Demo.OpenTelemetry/Demo.OpenTelemetry.csproj

RUN dotnet restore ./Demo.ProductCatalog.Api/Demo.ProductCatalog.Api.csproj

COPY ./Demo.ProductCatalog.Api/. ./Demo.ProductCatalog.Api/
COPY ./Demo.SharedModel/. ./Demo.SharedModel/
COPY ./Demo.OpenTelemetry/. ./Demo.OpenTelemetry/

RUN dotnet publish ./Demo.ProductCatalog.Api/Demo.ProductCatalog.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Demo.ProductCatalog.Api.dll"]