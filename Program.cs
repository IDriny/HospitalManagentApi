using System.Text;
using HospitalManagentApi.Configuration;
using HospitalManagentApi.Core.Contracts;
using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Persistence;
using HospitalManagentApi.Persistence.Configration;
using HospitalManagentApi.Persistence.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextServices(builder.Configuration)
    .AddIdentity()
    .AddControllerServices()
    .AddMapping()
    .AddAuthenticationService(builder.Configuration);

builder.Host.AddSerilog();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(OP =>
    {
        OP.SwaggerEndpoint("/swagger/v1/swagger.json","HospitalmanagementAPIV1");
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(OP =>
    {
        OP.SwaggerEndpoint("/swagger/v1/swagger.json", "HospitalmanagementAPIV1");
        OP.RoutePrefix = "";
    });
}



app.UseStaticFiles();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();