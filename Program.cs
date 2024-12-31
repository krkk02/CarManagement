
using Microsoft.AspNetCore.Cors.Infrastructure;
using System;
using CarManagement.Data;
using CarManagement.Services;
using CarManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsLocal", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddScoped<IGarageService, GarageService>();
            builder.Services.AddScoped<ICarService, CarService>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseCors("CorsLocal");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
