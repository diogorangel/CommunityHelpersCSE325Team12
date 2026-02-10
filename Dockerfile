# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 1. Copy everything first to ensure all folders exist in the build context
COPY . .

# 2. MOVE into the specific project folder (based on your GitHub structure)
WORKDIR "/src/CommunityHelpersCSE325Team12"

# 3. Restore dependencies pointing to the specific project file
RUN dotnet restore "CommunityHelpers.Blazor.csproj"

# 4. Build the project in Release mode
RUN dotnet build "CommunityHelpers.Blazor.csproj" -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish "CommunityHelpers.Blazor.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final Stage (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Create storage directory for SQLite and Uploads with proper permissions
RUN mkdir -p /app/wwwroot/uploads && chmod 777 /app/wwwroot/uploads

EXPOSE 80
EXPOSE 443

# Important: The ENTRYPOINT must match the generated DLL name (usually the project file name)
ENTRYPOINT ["dotnet", "CommunityHelpers.Blazor.dll"]