#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
RUN apt-get update && apt-get upgrade -y && rm -rf /var/lib/apt/lists/*
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
RUN apt-get update && apt-get upgrade -y && rm -rf /var/lib/apt/lists/*
WORKDIR /src
COPY ["MicroService.WebApi/MicroService.WebApi.csproj", "MicroService.WebApi/"]
COPY ["MicroService.Application/MicroService.Application.csproj", "MicroService.Application/"]
COPY ["MicroService.Domain/MicroService.Domain.csproj", "MicroService.Domain/"]
COPY ["MicroService.Infrastructure/MicroService.Infrastructure.csproj", "MicroService.Infrastructure/"]
COPY ["MicroService.Tests/MicroService.Tests.csproj", "MicroService.Tests/"]

RUN dotnet restore "MicroService.WebApi/MicroService.WebApi.csproj"
RUN dotnet restore "MicroService.Tests/MicroService.Tests.csproj"

COPY . .
WORKDIR "/src/MicroService.Tests"
RUN dotnet test MicroService.Tests.csproj -c Release

WORKDIR "/src/MicroService.WebApi"
RUN dotnet build "MicroService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroService.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroService.WebApi.dll"]