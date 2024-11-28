
using CRUD_ADVANCE.Data;
using CRUD_ADVANCE.DTOs.ProductDTO;
using CRUD_ADVANCE.DTOs.ProductDTO.Validation;
using CRUD_ADVANCE.Error;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CRUD_ADVANCE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IValidator<CreateProductDTO>, CreateProductDtoValidation>();
            builder.Host.UseSerilog((context,configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);

            });
            builder.Services.AddExceptionHandler<GlobalHandler>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            

            app.MapControllers();
            app.UseExceptionHandler(option => { });
            app.Run();
        }
    }
}
