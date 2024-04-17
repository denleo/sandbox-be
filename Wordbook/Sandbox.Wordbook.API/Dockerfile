FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Wordbook/Sandbox.Wordbook.API/Sandbox.Wordbook.API.csproj", "Wordbook/Sandbox.Wordbook.API/"]
COPY ["Common/Sandbox.Contracts/Sandbox.Contracts.csproj", "Common/Sandbox.Contracts/"]
COPY ["Common/Sandbox.Utility/Sandbox.Utility.csproj", "Common/Sandbox.Utility/"]
COPY ["Wordbook/Sandbox.Wordbook.Persistence/Sandbox.Wordbook.Persistence.csproj", "Wordbook/Sandbox.Wordbook.Persistence/"]
COPY ["Wordbook/Sandbox.Wordbook.Domain/Sandbox.Wordbook.Domain.csproj", "Wordbook/Sandbox.Wordbook.Domain/"]
COPY ["Wordbook/Sandbox.Wordbook.Application/Sandbox.Wordbook.Application.csproj", "Wordbook/Sandbox.Wordbook.Application/"]
RUN dotnet restore "Wordbook/Sandbox.Wordbook.API/Sandbox.Wordbook.API.csproj"
COPY . .
WORKDIR "/src/Wordbook/Sandbox.Wordbook.API"
RUN dotnet build "Sandbox.Wordbook.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Sandbox.Wordbook.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sandbox.Wordbook.API.dll"]
