# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Dependences
COPY ["CommunityHelpers.Web/CommunityHelpers.Blazor.csproj", "CommunityHelpers.Web/"]
RUN dotnet restore "CommunityHelpers.Web/CommunityHelpers.Blazor.csproj"

# Files
COPY . .
WORKDIR "/src/CommunityHelpers.Web"
RUN dotnet build "CommunityHelpers.Blazor.csproj" -c Release -o /app/build

# Publishers
FROM build AS publish
RUN dotnet publish "CommunityHelpers.Blazor.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final Stage (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Folerder
RUN mkdir -p /app/wwwroot/uploads && chmod 777 /app/wwwroot/uploads

# Port folder Render
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "CommunityHelpers.Blazor.dll"]