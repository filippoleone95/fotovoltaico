# Dockerfile for PersistenceService
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS="http://0.0.0.0:80"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore PersistenceService.csproj
RUN dotnet build PersistenceService.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish PersistenceService.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY fotovoltaico.db /app/fotovoltaico.db
ENTRYPOINT ["dotnet", "PersistenceService.dll"]
