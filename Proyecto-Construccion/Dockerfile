# Imagen base para el runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Imagen para build del proyecto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia todo el código fuente
COPY . .

# Restaura dependencias y compila
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Imagen final de runtime
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Ejecuta el proyecto
ENTRYPOINT ["dotnet", "Proyecto-Construccion.dll"]
