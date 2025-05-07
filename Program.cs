using HospitalManagentApi.Core.Contracts;
using HospitalManagentApi.Persistence;
using HospitalManagentApi.Persistence.Configration;
using HospitalManagentApi.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("HospitalDbConnectionString");
builder.Services.AddDbContext<HospitalDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod());
});



builder.Host.UseSerilog((ctx, LoggerConfiguration) =>
    LoggerConfiguration.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddAutoMapper(typeof(MapperConfigration));

builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));

builder.Services.AddScoped<IAppointmentRepo, AppointmentRepo>();

builder.Services.AddScoped<IClinicDoctorRepo, ClinicDoctorRepo>();

builder.Services.AddScoped<IClinicRepo, ClinicRepo>();

builder.Services.AddScoped<IDoctorRepo, DoctorRepo>();

builder.Services.AddScoped<IPatientRepo, PatientRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();