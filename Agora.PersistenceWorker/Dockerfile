FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-jammy-chiseled AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5223
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Agora.PersistenceWorker/Agora.PersistenceWorker.csproj", "Agora.PersistenceWorker/"]
COPY ["Agora.ServiceDefaults/Agora.ServiceDefaults.csproj", "Agora.ServiceDefaults/"]
RUN dotnet restore "Agora.PersistenceWorker/Agora.PersistenceWorker.csproj"
COPY . .
WORKDIR "/src/Agora.PersistenceWorker"
RUN dotnet build "Agora.PersistenceWorker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Agora.PersistenceWorker.csproj" -c $BUILD_CONFIGURATION -o /app/publish \
   -r linux-x64 \
   --self-contained true \
 #  -p:PublishTrimmed=true \
   -p:PublishSingleFile=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./Agora.PersistenceWorker"]