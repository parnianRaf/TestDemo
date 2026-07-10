FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY WeatherApi/WeatherApi.csproj .
RUN dotnet restore

COPY WeatherApi/ .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 9779
ENTRYPOINT ["dotnet", "WeatherApi.dll"]
