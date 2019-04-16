
docker pull mcr.microsoft.com/mssql/server

docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=ICJ5*cookies' -e 'MSSQL_PID=Express' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest-ubuntu 

docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=ICJ5*cookies' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest

dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Autofac.Extensions.DependencyInjection (https://autofaccn.readthedocs.io/en/latest/integration/netcore.html)
dotnet add package AutoMapper --version 8.0.0
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 6.0.0
dotnet add package Microsoft.Extensions.Logging.Console
dotnet add package Swashbuckle.AspNetCore
dotnet restore

dotnet add package Moq
dotnet add package Microsoft.VisualStudio.UnitTesting
dotnet add package NSubstitute

dotnet ef migrations add InitialCreate
dotnet ef database update

git remote add origin https://github.com/mtrinder/TheCodeCampDemo.git
git push -u origin master