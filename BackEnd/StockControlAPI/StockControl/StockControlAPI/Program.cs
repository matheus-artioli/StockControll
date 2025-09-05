using Microsoft.EntityFrameworkCore;
using StockControlAPI.Data;
using StockControlAPI.Repository;

namespace StockControlAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 32));
            builder.Services.AddScoped<ProdutosRepository, EstoqueRepository>();
            builder.Services.AddScoped<FornecedoresRepository, PorFornecedorRepository>();
            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseMySql(connectionString, serverVersion)
            );

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseCors(policy =>
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader() 
            );

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}