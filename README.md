## I'm glad to share that I finished working on my first web app using ASP.NET Core 


### Introduction
---
- It's simple an API for a hospital management system
- Here is tha like for swagger documentation
    - https://hospitalmanagentapi20250601133735-f4fwc9f7duanb5b3.israelcentral-01.azurewebsites.net
- and here the web app that used my API
   - https://hospital-project-ruddy.vercel.app/
- THat is my first project and I'm sure it's not the last, so stay tuned from more projects
- this project have been developed in cooperation with my dear friend and cooworker:  [Mohammed Tark](https://github.com/sezeef)


### Getting started
---
- first you need create MSSQL connection
- then add the cooection string to `appsettings.Production.json` file
- then instll extension `Microsoft.EntityFrameworkCore` , `Microsoft.EntityFrameworkCore.SqlServer` , `Microsoft.EntityFrameworkCore.Tools` ,`Microsoft.VisualStudio.Web.CodeGeneration.Design` if not already installed
- then `add-migration {migration name}` throw Package Manager Console
- finally apply these migration to the data base using `update-databse` comand
