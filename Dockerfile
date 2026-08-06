# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copiar primero los archivos necesarios para restaurar dependencias
COPY BibliotecaAPI.sln ./
COPY BibliotecaAPI/BibliotecaAPI.csproj ./BibliotecaAPI/
RUN dotnet restore BibliotecaAPI/BibliotecaAPI.csproj

# Copiar el resto del código fuente
COPY . ./

# Publicar la API
RUN dotnet publish BibliotecaAPI/BibliotecaAPI.csproj \
    -c Release \
    -o /app/out \
    --no-restore

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
COPY --from=build /app/out ./

ENV ASPNETCORE_HTTP_PORTS=8000

EXPOSE 8000

ENTRYPOINT ["dotnet", "BibliotecaAPI.dll"]