# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 1. Copy everything
COPY . .

# 2. Use a shell command to find the .csproj file and restore it
# This handles any nested folder structure automatically
RUN dotnet restore $(find . -name "*.csproj")

# 3. Build using the project file found by the system
RUN dotnet build $(find . -name "*.csproj") -c Release -o /app/build

# Publish Stage
FROM build AS publish
# We use the same 'find' logic to ensure we publish the right project
RUN dotnet publish $(find . -name "*.csproj") -c Release -o /app/publish /p:UseAppHost=false

# Final Stage (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Create storage directory
RUN mkdir -p /app/wwwroot/uploads && chmod 777 /app/wwwroot/uploads

EXPOSE 80
EXPOSE 443

# Match the output DLL. Based on your project, it should be:
ENTRYPOINT ["dotnet", "CommunityHelpers.Blazor.dll"]