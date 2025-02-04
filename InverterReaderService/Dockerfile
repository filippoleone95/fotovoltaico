# Dockerfile for InverterReaderService
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS="http://0.0.0.0:80"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore InverterReaderService.csproj
RUN dotnet build InverterReaderService.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish InverterReaderService.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "InverterReaderService.dll"]
