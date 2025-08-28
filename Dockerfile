# syntax=docker/dockerfile:1

# Comments are provided throughout this file to help you get started.
# If you need more help, visit the Dockerfile reference guide at
# https://docs.docker.com/go/dockerfile-reference/

# Want to help us make this template better? Share your feedback here: https://forms.gle/ybq9Krt8jtBL3iCk7

################################################################################

# Learn about building .NET container images:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md

# Create a stage for building the application.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /source/Investment.FVCalculator.Api
EXPOSE 8080
EXPOSE 8081

COPY . /source

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Investment.FVCalculator.Api.csproj", "."]
RUN dotnet restore "./././Investment.FVCalculator.Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Investment.FVCalculator.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Investment.FVCalculator.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Investment.FVCalculator.Api.dll"]
