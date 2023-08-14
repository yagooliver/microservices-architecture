FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /src
COPY ./*.sln ./
COPY ["./Gateway/MicroserviceArchitecture.Api.Gateway/MicroserviceArchitecture.Api.Gateway.csproj", "./MicroserviceArchitecture.Api.Gateway/" ]
RUN dotnet restore "./MicroserviceArchitecture.Api.Gateway/MicroserviceArchitecture.Api.Gateway.csproj"
COPY . .
RUN dotnet publish -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app/publish
COPY --from=build-env /app/publish .
ENV ASPNETCORE_URLS=http://+:30001;https://+:3001
EXPOSE 30001/tcp
EXPOSE 3001/tcp
ENTRYPOINT ["dotnet", "MicroserviceArchitecture.Api.Gateway.dll","--server.urls", "http://*:30001;https://*:3001"]