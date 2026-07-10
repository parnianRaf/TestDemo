FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY publish/ .

EXPOSE 9779
ENTRYPOINT ["dotnet", "WeatherApi.dll"]
