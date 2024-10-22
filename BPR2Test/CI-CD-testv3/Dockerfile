# Use the .NET 6 ASP.NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Set the environment variable to listen on port 80
ENV ASPNETCORE_URLS=http://+:80

# Use the .NET 6 SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the entire solution and restore dependencies
COPY ["BPR2Test.sln", "./"]
COPY ["CI-CD-testv3/CI-CD-testv3.csproj", "CI-CD-testv3/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore

# Copy all project files and build the solution
COPY . .
WORKDIR "/src/CI-CD-testv3"
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Create the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CI-CD-testv3.dll"]
