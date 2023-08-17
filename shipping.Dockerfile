FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /src
COPY ./*.sln ./
COPY ["./MicroserviceArchitecture.Services.Shipping.Service/MicroserviceArchitecture.Services.Shipping.Service.csproj", "./MicroserviceArchitecture.Services.Shipping.Service/" ]
RUN dotnet restore "./MicroserviceArchitecture.Services.Shipping.Service/MicroserviceArchitecture.Services.Shipping.Service.csproj"
COPY . .
RUN dotnet publish -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app/publish
COPY --from=build-env /app/publish .
ENV ASPNETCORE_URLS=http://+:30004;https://+:3004
EXPOSE 30004/tcp
EXPOSE 3004/tcp
ENTRYPOINT ["dotnet", "MicroserviceArchitecture.Services.Shipping.Service.dll","--server.urls", "http://*:30004;https://*:3004"]