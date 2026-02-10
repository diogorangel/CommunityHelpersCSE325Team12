# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 1. Copy only the project file first (improves build caching)
# The asterisk helps find the file if the path varies slightly
COPY ["CommunityHelpers.Web/*.csproj", "CommunityHelpers.Web/"]

# 2. Restore dependencies
RUN dotnet restore "CommunityHelpers.Web/CommunityHelpers.Blazor.csproj"

# 3. Copy ALL repository content
COPY . .

# 4. Set the working directory to the web project
WORKDIR "/src/CommunityHelpers.Web"
RUN dotnet build "CommunityHelpers.Blazor.csproj" -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish "CommunityHelpers.Blazor.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final Stage (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Create the uploads folder and grant permissions (Crucial for Render/Linux)
RUN mkdir -p /app/wwwroot/uploads && chmod 777 /app/wwwroot/uploads

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "CommunityHelpers.Blazor.dll"]