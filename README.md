# My First production level Project
**I'm glad to share that I've finished working on my first web app using ASP.NET Core** 



### Introduction
---
- It's simply an API for a hospital management system
- Here is tha like for swagger documentation
    - https://hospitalmanagentapi20250601133735-f4fwc9f7duanb5b3.israelcentral-01.azurewebsites.net
- and here the web app that used my API
   - https://hospital-project-ruddy.vercel.app/
- This is my first project and I'm sure it's not the last, so stay tuned for more projects
- this project have been developed in cooperation with my dear friend and cooworker:  [Mohammed Tark](https://github.com/sezeef)






### Getting started
---
- first you need create MSSQL connection
- then add the cooection string to `appsettings.Production.json` file
- then instll extension `Microsoft.EntityFrameworkCore` , `Microsoft.EntityFrameworkCore.SqlServer` , `Microsoft.EntityFrameworkCore.Tools` ,`Microsoft.VisualStudio.Web.CodeGeneration.Design` if not already installed
- then `add-migration {migration name}` throw Package Manager Console
- finally apply these migration to the data base using `update-databse` comand
  





### for better understanding
---
to learn more about ASP.NET Core And web APIs
    - visit swagger documentaion [Swagger](https://swagger.io/docs/)
    - Read microsoft documentation for [ASP.NET Core web APIs](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-9.0&tabs=visual-studio)






### Deployed on Azure
---
the best way to deploy any ASP.NET Core Web APP is to use [Azure web app](https://learn.microsoft.com/en-us/azure/app-service/) 
    - To deploy this ASP.NET Core web app to Azure, use the `Azure App Service` extension in Visual Studio Code or run `az webapp up` from the CLI after configuring your Azure subscription.  
