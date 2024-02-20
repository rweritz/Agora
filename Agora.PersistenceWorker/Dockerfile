FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Agora.PersistenceWorker/Agora.PersistenceWorker.csproj", "Agora.PersistenceWorker/"]
RUN dotnet restore "Agora.PersistenceWorker/Agora.PersistenceWorker.csproj"
COPY . .
WORKDIR "/src/Agora.PersistenceWorker"
RUN dotnet build "Agora.PersistenceWorker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Agora.PersistenceWorker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Agora.PersistenceWorker.dll"]
