FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
COPY appsettings.json /app/
COPY Outputs /app/Outputs

ENTRYPOINT ["dotnet", "EarthQuakeData.dll"]
#docker build -t image-name -f Dockerfile .