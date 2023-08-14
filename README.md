# microservices-architecture
In development

# Instructions
First of all you must set the certificate to run over https

```console
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p <CREDENTIAL_PLACEHOLDER>
dotnet dev-certs https --trust
```

* OBS: you must set the credential that you use, in this project on docker-compose.yml file the credential that was set is dev123

After that, you must run the following commands in the root folder to run the containers

```console
docker-compose build
docker-compose up -d
```

