FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Host/Host.csproj", "Host/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain.Ports/Domain.Ports.csproj", "Domain.Ports/"]
COPY ["Domain.Entities/Domain.Entities.csproj", "Domain.Entities/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Application.API/Application.API.csproj", "Application.API/"]
COPY ["Infra.Adapters/Infra.Adapters.csproj", "Infra.Adapters/"]
RUN dotnet restore "Host/Host.csproj"
COPY . .
WORKDIR "/src/Host"
RUN dotnet build "Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Host.dll"]
