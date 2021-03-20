FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["microservice1/microservice1.csproj", "microservice1/"]
RUN dotnet restore "microservice1\microservice1.csproj"
COPY . .
WORKDIR "/src/microservice1"
RUN dotnet build "microservice1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "microservice1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "microservice1.dll"]
