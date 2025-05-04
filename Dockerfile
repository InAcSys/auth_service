# Dockerfile (ubicado en el directorio AuthenticationService/)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de proyecto necesarios
COPY ["Presentation/Presentation.csproj", "Presentation/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["AuthenticationService.sln", "."]

# Restaurar dependencias
RUN dotnet restore "AuthenticationService.sln"

# Copiar todo el código fuente
COPY . .

# Build solución
RUN dotnet build "AuthenticationService.sln" -c Release -o /app/build

# Publicar proyecto principal
RUN dotnet publish "Presentation/Presentation.csproj" -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Presentation.dll"]