FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:6.0.302 AS build
WORKDIR /build
COPY Exemplo5_Aspnet_ELK.csproj Exemplo5_Aspnet_ELK.csproj
RUN dotnet restore  
COPY . .
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Exemplo5_Aspnet_ELK.dll"]