FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["backend/src/Streakly.Core/Streakly.Core.csproj", "backend/src/Streakly.Core/"]
COPY ["backend/src/Streakly.Application/Streakly.Application.csproj", "backend/src/Streakly.Application/"]
COPY ["backend/src/Streakly.Infrastructure/Streakly.Infrastructure.csproj", "backend/src/Streakly.Infrastructure/"]
COPY ["backend/src/Streakly.Api/Streakly.Api.csproj", "backend/src/Streakly.Api/"]
RUN dotnet restore "backend/src/Streakly.Api/Streakly.Api.csproj"
COPY . .
WORKDIR "/src/backend/src/Streakly.Api"
RUN dotnet build "./Streakly.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Streakly.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Streakly.Api.dll"]
