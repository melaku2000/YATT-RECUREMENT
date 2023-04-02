#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CatagoryService/CatagoryService.csproj", "CatagoryService/"]
COPY ["YattCommon/YattCommon.csproj", "YattCommon/"]
RUN dotnet restore "CatagoryService/CatagoryService.csproj"
COPY . .
WORKDIR "/src/CatagoryService"
RUN dotnet build "CatagoryService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CatagoryService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatagoryService.dll"]