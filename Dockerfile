# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 1. Copy the project file using the correct folder name
# This matches your GitHub structure: CommunityHelpersCSE325Team12/CommunityHelpers.Blazor.csproj
COPY ["CommunityHelpersCSE325Team12/CommunityHelpers.Blazor.csproj", "CommunityHelpersCSE325Team12/"]

# 2. Restore dependencies
RUN dotnet restore "CommunityHelpersCSE325Team12/CommunityHelpers.Blazor.csproj"

# 3. Copy everything else in the repository
COPY . .

# 4. Move to the project folder and build
WORKDIR "/src/CommunityHelpersCSE325Team12"
RUN dotnet build "CommunityHelpers.Blazor.csproj" -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish "CommunityHelpers.Blazor.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final Stage (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Create the uploads folder for images and database
RUN mkdir -p /app/wwwroot/uploads && chmod 777 /app/wwwroot/uploads

EXPOSE 80
EXPOSE 443

# Ensure the DLL name matches your project output
ENTRYPOINT ["dotnet", "CommunityHelpers.Blazor.dll"]