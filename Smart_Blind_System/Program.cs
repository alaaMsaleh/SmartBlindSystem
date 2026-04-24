using BlindSystem.Domain.Entities;
using BlindSystem.Domain.Interfaces;
using BlindSystem.Domain.Service_Contract;
using BlindSystem.Infrastructure.Data.DBContext;
using BlindSystem.Infrastructure.Repositories;
using BlindSystem.Service.AuthenSystem;
using BlindSystem.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Smart_Blind_System.API.MailService;
using System.Text;


namespace Smart_Blind_System
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            builder.WebHost.UseUrls($"http://*:{port}");
            builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped(typeof(IAuth), typeof(Auth));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddTransient<IMailService, MailService>();




            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
      .AddEntityFrameworkStores<BlindSystemDbContext>()
      .AddDefaultTokenProviders();


            var durationStr = builder.Configuration["JWT:DurationInDays"] ?? "7";
            var duration = double.Parse(durationStr);

            //Add JWT Setting
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],



                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"])),


                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    RequireSignedTokens = true,
                };

            })
            .AddGoogle(options =>
                 {
                     options.ClientId = builder.Configuration["Google:ClientId"];
                     options.ClientSecret = builder.Configuration["Google:ClientSecret"];


                 });


            builder.Services.Configure<IdentityOptions>(options =>
            {

                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
            });
            //Login With Google



            #region Configration DateBase
            //Default DataBase
            builder.Services.AddDbContext<BlindSystemDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

            builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(1);
            });

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Smart_Blind_System.API", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token."
                });


                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });

            #endregion
            var app = builder.Build();

            #region Configure_MiddelWare

            // شيلنا الـ IF عشان يشتغل على السيرفر
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            #endregion

            app.MapControllers();

            app.Run();
        }
    }
}
