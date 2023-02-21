#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HiveBridge/HiveBridge.csproj", "HiveBridge/"]
RUN dotnet restore "HiveBridge/HiveBridge.csproj"
COPY . .
WORKDIR "/src/HiveBridge"
RUN dotnet build "HiveBridge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HiveBridge.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HiveBridge.dll"]