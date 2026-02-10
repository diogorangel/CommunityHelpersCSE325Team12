# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 1. Find ANY .csproj file in the current directory or subdirectories and copy it to the root
COPY *.sln ./
COPY **/*.csproj ./
# This command moves the csproj to where the build expects it if it's not in a subfolder
RUN find . -name "*.csproj" -exec cp {} . \;

# 2. Restore dependencies
RUN dotnet restore

# 3. Copy everything from the repository
COPY . .

# 4. Build the project
# This uses 'dotnet build' on the current directory
RUN dotnet build -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Final Stage (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Create storage for SQLite and Images
RUN mkdir -p /app/wwwroot/uploads && chmod 777 /app/wwwroot/uploads

EXPOSE 80
EXPOSE 443

# Match this to your actual output DLL name
ENTRYPOINT ["dotnet", "CommunityHelpers.Blazor.dll"]