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
using System.Text;
using Microsoft.AspNetCore.OData;

namespace HospitalManagentApi.Configuration
{
    public static class Dependencies
    {
        public static IServiceCollection AddDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            var connectionString = configuration.GetConnectionString("DefaultSQLConnection");
            services.AddDbContext<HospitalDbContext>(options => options.UseSqlServer(connectionString));
            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<ApiUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<ApiUser>>("HospitalManagementAPI")
                .AddEntityFrameworkStores<HospitalDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }

        public static IServiceCollection AddControllerServices(this IServiceCollection services)
        {
            services.AddControllers().AddOData(op =>
            {
                op.OrderBy().Select().Filter();
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", b => b.AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod());
            });
            return services;
        }

        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperConfiguration));

            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));

            services.AddScoped<IAppointmentRepo, AppointmentRepo>();

            services.AddScoped<IClinicDoctorRepo, ClinicDoctorRepo>();

            services.AddScoped<IClinicRepo, ClinicRepo>();

            services.AddScoped<IDoctorRepo, DoctorRepo>();

            services.AddScoped<IPatientRepo, PatientRepo>();

            services.AddScoped<IDiagnosisRepo, DiagnosisRepo>();

            services.AddScoped<IPrescriptionRepo, PrescriptionRepo>();

            services.AddScoped<ILabRepo, LabRepo>();

            services.AddScoped<IAuthManager, AuthManager>();

            return services;
        }

        public static IServiceCollection AddAuthenticationService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                //"Bearer"
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                };
            });
            return services;
        }

       
    }
}
