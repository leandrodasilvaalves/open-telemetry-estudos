FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0.302 AS build
WORKDIR /build
COPY ./Demo.Payment.Api/Demo.Payment.Api.csproj ./Demo.Payment.Api/Demo.Payment.Api.csproj
COPY ./Demo.SharedModel/Demo.SharedModel.csproj ./Demo.SharedModel/Demo.SharedModel.csproj
RUN dotnet restore ./Demo.Payment.Api/Demo.Payment.Api.csproj
COPY ./Demo.Payment.Api/. ./Demo.Payment.Api/
COPY ./Demo.SharedModel/. ./Demo.SharedModel/
RUN dotnet publish ./Demo.Payment.Api/Demo.Payment.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Demo.Payment.Api.dll"]