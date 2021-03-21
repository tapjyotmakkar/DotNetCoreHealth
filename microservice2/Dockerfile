FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["microservice2/microservice2.csproj", "microservice2/"]
RUN dotnet restore "microservice2\microservice2.csproj"
COPY . .
WORKDIR "/src/microservice2"
RUN dotnet build "microservice2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "microservice2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "microservice2.dll"]
