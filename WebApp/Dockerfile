# Utilizzo dell'immagine .NET SDK per il build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copio e ripristino i pacchetti
COPY WebApp/WebApp.csproj WebApp/
RUN dotnet restore "WebApp/WebApp.csproj"

# Copio tutto e compilo il progetto
COPY . .
WORKDIR /src/WebApp
RUN dotnet build "WebApp.csproj" -c Release -o /app/build

# Pubblico l'applicazione
FROM build AS publish
RUN dotnet publish "WebApp.csproj" -c Release -o /app/publish

# Utilizzo dell'immagine runtime .NET
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Imposto la variabile di ambiente per ASP.NET Core
ENV ASPNETCORE_URLS="http://0.0.0.0:80"

# Copio i file pubblicati
COPY --from=publish /app/publish .

# Avvio dell'applicazione
ENTRYPOINT ["dotnet", "WebApp.dll"]