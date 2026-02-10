# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .

# Restore and build using automatic file search (finds the .csproj regardless of folder structure)
RUN dotnet restore $(find . -name "*.csproj")
RUN dotnet build $(find . -name "*.csproj") -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish $(find . -name "*.csproj") -c Release -o /app/publish /p:UseAppHost=false

# Final Stage (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Local uploads folder (inside the container's writable layer)
RUN mkdir -p wwwroot/uploads && chmod 777 wwwroot/uploads

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "CommunityHelpers.Blazor.dll"]