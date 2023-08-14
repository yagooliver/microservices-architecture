FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /src
RUN dotnet tool install --global dotnet-certificate-tool
RUN dotnet dev-certs https --trust
COPY ./*.sln ./
COPY ./Common/MicroserviceArchitecture.Common.Models/*.csproj ./MicroserviceArchitecture.Common.Models/
COPY ["./Microservices/Customer/MicroserviceArchitecture.Services.Customer.Model/MicroserviceArchitecture.Services.Customer.Model.csproj", "./MicroserviceArchitecture.Services.Customer.Model/" ]
COPY ["./Microservices/Customer/MicroserviceArchitecture.Services.Customer.Application/MicroserviceArchitecture.Services.Customer.Application.csproj", "./MicroserviceArchitecture.Services.Customer.Application/" ]
COPY ["./Microservices/Customer/MicroserviceArchitecture.Services.Customer.Infra.Redis/MicroserviceArchitecture.Services.Customer.Infra.Redis.csproj", "./MicroserviceArchitecture.Services.Customer.Infra.Redis/" ]
COPY ["./Microservices/Customer/MicroserviceArchitecture.Services.Customer.Contracts/MicroserviceArchitecture.Services.Customer.Contracts.csproj", "./MicroserviceArchitecture.Services.Customer.Contracts/" ]
COPY ["./Microservices/Customer/MicroserviceArchitecture.Services.Customer.Infra.SqlServer/MicroserviceArchitecture.Services.Customer.Infra.SqlServer.csproj", "./MicroserviceArchitecture.Services.Customer.Infra.SqlServer/" ]
COPY ["./Microservices/Customer/MicroserviceArchitecture.Services.Customer.Service/MicroserviceArchitecture.Services.Customer.Service.csproj", "./MicroserviceArchitecture.Services.Customer.Service/" ]
RUN dotnet restore "./MicroserviceArchitecture.Services.Customer.Service/MicroserviceArchitecture.Services.Customer.Service.csproj"
COPY . .
RUN dotnet publish -c Release -o /publish
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /publish
COPY --from=build-env /publish .
ENV ASPNETCORE_URLS=http://+:30002;https://+:3002
EXPOSE 30002/tcp
EXPOSE 3002/tcp
ENTRYPOINT ["dotnet", "MicroserviceArchitecture.Services.Customer.Service.dll","--server.urls", "http://*:30002;https://*:3002"]