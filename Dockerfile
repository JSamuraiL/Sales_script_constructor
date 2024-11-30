
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["SalesScriptConstructor/SalesScriptConstructor.API.csproj", "SalesScriptConstructor/"]
COPY ["Infrastructure/SalesScriptConstructor.Infrastructure.csproj", "Infrastructure/"]
COPY ["Domain/SalesScriptConstructor.Domain.csproj", "Domain/"]

RUN dotnet restore "SalesScriptConstructor/SalesScriptConstructor.API.csproj"

COPY . .

RUN dotnet build "SalesScriptConstructor/SalesScriptConstructor.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SalesScriptConstructor/SalesScriptConstructor.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SalesScriptConstructor.API.dll"]
