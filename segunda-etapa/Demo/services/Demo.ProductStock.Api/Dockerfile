FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0.302 AS build
WORKDIR /build
COPY ./Demo.ProductStock.Api/Demo.ProductStock.Api.csproj ./Demo.ProductStock.Api/Demo.ProductStock.Api.csproj
COPY ./Demo.SharedModel/Demo.SharedModel.csproj ./Demo.SharedModel/Demo.SharedModel.csproj
RUN dotnet restore ./Demo.ProductStock.Api/Demo.ProductStock.Api.csproj
COPY ./Demo.ProductStock.Api/. ./Demo.ProductStock.Api/
COPY ./Demo.SharedModel/. ./Demo.SharedModel/
RUN dotnet publish ./Demo.ProductStock.Api/Demo.ProductStock.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Demo.ProductStock.Api.dll"]