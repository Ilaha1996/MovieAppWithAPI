using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Business;
using MovieApp.Business.DTOs.MovieDTOs;
using MovieApp.Business.MappingProfiles;
using MovieApp.Core.Entities;
using MovieApp.Data;
using MovieApp.Data.DAL;
using System.Text;
namespace MovieAppAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssembly(typeof(MovieCreateDtoValidator).Assembly);
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredUniqueChars = 2;
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredLength = 8;

                opt.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            builder.Services.AddAutoMapper(opt =>
            {
                opt.AddProfile<MapProfile>();
            });


            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidIssuer = "https://localhost:7006/",
                    ValidAudience = "https://localhost:7006/",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aba72807-29ef-4322-af8b-a69906cdba01Agalar")),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddRepos(builder.Configuration.GetConnectionString("default"));
            builder.Services.AddServices();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
