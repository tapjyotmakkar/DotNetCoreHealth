FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ServerStatusService/ServerStatusService.csproj", "ServerStatusService/"]
RUN dotnet restore "ServerStatusService\ServerStatusService.csproj"
COPY . .
WORKDIR "/src/ServerStatusService"
RUN dotnet build "ServerStatusService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServerStatusService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServerStatusService.dll"]
