# Etapa base para tiempo de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar todo el proyecto
COPY . .

# Restaurar paquetes y compilar
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Etapa final: imagen liviana solo con lo necesario
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Punto de entrada: tu proyecto
ENTRYPOINT ["dotnet", "ProyectoAPI.dll"]
