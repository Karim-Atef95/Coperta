using Coperta.Data;
using Microsoft.EntityFrameworkCore;

namespace Coperta
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            //k connection string declared then db context declared & configured

            var connectionString = builder.Configuration
                .GetConnectionString("DefaultConnection"); 

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            // Add services to the container.
            
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

            app.Run();
        }
    }
}