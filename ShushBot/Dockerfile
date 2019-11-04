FROM mcr.microsoft.com/dotnet/core/sdk:3.0.100-alpine3.9 AS build
WORKDIR /src
COPY . .
RUN dotnet restore ShushBot.csproj
RUN dotnet build ShushBot.csproj -c Release
RUN dotnet publish ShushBot.csproj -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0.0-alpine3.9 AS runtime
WORKDIR /app
COPY --from=build /src/publish .
ENTRYPOINT ["dotnet", "ShushBot.dll"]
