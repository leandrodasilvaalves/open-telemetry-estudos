FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0.302 AS build
WORKDIR /build
COPY ./Demo.Emails.Worker/Demo.Emails.Worker.csproj ./Demo.Emails.Worker/Demo.Emails.Worker.csproj
COPY ./Demo.SharedModel/Demo.SharedModel.csproj ./Demo.SharedModel/Demo.SharedModel.csproj
RUN dotnet restore ./Demo.Emails.Worker/Demo.Emails.Worker.csproj
COPY ./Demo.Emails.Worker/. ./Demo.Emails.Worker/
COPY ./Demo.SharedModel/. ./Demo.SharedModel/
RUN dotnet publish ./Demo.Emails.Worker/Demo.Emails.Worker.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Demo.Emails.Worker.dll"]