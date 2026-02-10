# Build Stage - Using .NET 10 SDK
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# 1. Copy everything
COPY . .

# 2. Find and restore the project
RUN dotnet restore $(find . -name "*.csproj")

# 3. Build the project
RUN dotnet build $(find . -name "*.csproj") -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish $(find . -name "*.csproj") -c Release -o /app/publish /p:UseAppHost=false

# Final Stage (Runtime) - Using .NET 10 ASP.NET Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Create storage directory for SQLite and Images
RUN mkdir -p /app/wwwroot/uploads && chmod 777 /app/wwwroot/uploads

EXPOSE 80
EXPOSE 443

# Start the application
ENTRYPOINT ["dotnet", "CommunityHelpers.Blazor.dll"]