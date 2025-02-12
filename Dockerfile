# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Set ASP.NET Core URLs to bind properly in container
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_RUNNING_IN_CONTAINER=true

# Install PostgreSQL client libraries
RUN apt-get update && apt-get install -y libpq-dev && apt-get clean

# Build image with SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy and restore dependencies
COPY ["ERPSYS.csproj", "./"]
RUN dotnet restore "ERPSYS.csproj"

# Copy the entire source code
COPY . .

# Build the project
RUN dotnet build "ERPSYS.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "ERPSYS.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Entry point
CMD ["dotnet", "ERPSYS.dll"]
