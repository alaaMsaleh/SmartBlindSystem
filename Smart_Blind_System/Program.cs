

using BlindSystem.Domain.Entities;
using BlindSystem.Domain.Interfaces;
using BlindSystem.Infrastructure.Data.DBContext;
using BlindSystem.Infrastructure.Repositories;
using BlindSystem.Service.AuthenSystem;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Smart_Blind_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped(typeof(IAuth), typeof(Auth));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));




            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
      .AddEntityFrameworkStores<BlindSystemDbContext>()
      .AddDefaultTokenProviders();

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
                    ValidIssuer = builder.Configuration["JWT:Issure"],

                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValiedAudience"],


                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"])),


                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    RequireSignedTokens = true,

                };
            });



            #region Configration DateBase
            //Default DataBase
            builder.Services.AddDbContext<BlindSystemDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //Identity DataBase 


            #endregion
            var app = builder.Build();

            #region Configure_MiddelWare
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            #endregion

            app.MapControllers();

            app.Run();
        }
    }
}
