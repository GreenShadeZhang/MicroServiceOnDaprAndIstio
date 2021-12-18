#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MicroService.HttpB/MicroService.HttpB.csproj", "MicroService.HttpB/"]
RUN dotnet restore "MicroService.HttpB/MicroService.HttpB.csproj"
COPY . .
WORKDIR "/src/MicroService.HttpB"
RUN dotnet build "MicroService.HttpB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroService.HttpB.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroService.HttpB.dll"]