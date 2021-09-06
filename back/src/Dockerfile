FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copiar csproj e restaurar dependencias
FROM mcr.microsoft.com/dotnet/nightly/sdk:5.0 AS build
WORKDIR /src
COPY ["ProEventos.API/ProEventos.API.csproj", "ProEventos.API/"]
COPY ["ProEventos.Application/ProEventos.Application.csproj", "ProEventos.Application/"]
COPY ["ProEventos.Domain/ProEventos.Domain.csproj", "ProEventos.Domain/"]
COPY ["ProEventos.Persistence/ProEventos.Persistence.csproj", "ProEventos.Persistence/"]
RUN dotnet restore "ProEventos.API/ProEventos.API.csproj"

# Build da aplicacao
COPY . .
WORKDIR "/src/ProEventos.API"
RUN dotnet build "ProEventos.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProEventos.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /appc
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProEventos.API.dll"]