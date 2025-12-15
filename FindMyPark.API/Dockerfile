# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FindMyPark.API.csproj", "./"]
RUN dotnet restore "./FindMyPark.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "FindMyPark.API.csproj" -c Release -o /app/build
RUN dotnet publish "FindMyPark.API.csproj" -c Release -o /app/publish

# Run Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "FindMyPark.API.dll"]
