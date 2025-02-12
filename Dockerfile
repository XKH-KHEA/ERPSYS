# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# USER $APP_UID  # Remove this unless it's specifically required for your environment
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Install PostgreSQL client libraries
RUN apt-get update && apt-get install -y libpq-dev
# Copy the .csproj file to the container and restore dependencies
COPY ["./ERPSYS.csproj", "./"]
RUN dotnet restore "./ERPSYS.csproj"

# Now copy the entire source code
COPY . .

# Build the project
WORKDIR "/src"
RUN dotnet build "./ERPSYS.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ERPSYS.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ERPSYS.dll"]
