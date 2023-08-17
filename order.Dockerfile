FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /src
COPY ./*.sln ./
COPY ["./MicroserviceArchitecture.Services.Order.Service/MicroserviceArchitecture.Services.Order.Service.csproj", "./MicroserviceArchitecture.Services.Order.Service/" ]
RUN dotnet restore "./MicroserviceArchitecture.Services.Order.Service/MicroserviceArchitecture.Services.Order.Service.csproj"
COPY . .
RUN dotnet publish -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app/publish
COPY --from=build-env /app/publish .
ENV ASPNETCORE_URLS=http://+:30003;https://+:3003
EXPOSE 30003/tcp
EXPOSE 3003/tcp
ENTRYPOINT ["dotnet", "MicroserviceArchitecture.Services.Order.Service.dll","--server.urls", "http://*:30003;https://*:3003"]