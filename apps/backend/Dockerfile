FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["apps/backend/Streakly.Core/Streakly.Core.csproj", "apps/backend/Streakly.Core/"]
COPY ["apps/backend/Streakly.Application/Streakly.Application.csproj", "apps/backend/Streakly.Application/"]
COPY ["apps/backend/Streakly.Infrastructure/Streakly.Infrastructure.csproj", "apps/backend/Streakly.Infrastructure/"]
COPY ["apps/backend/Streakly.Api/Streakly.Api.csproj", "apps/backend/Streakly.Api/"]
RUN dotnet restore "apps/backend/Streakly.Api/Streakly.Api.csproj"
COPY . .
WORKDIR "/src/apps/backend/Streakly.Api"
RUN dotnet build "./Streakly.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Streakly.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Streakly.Api.dll"]
