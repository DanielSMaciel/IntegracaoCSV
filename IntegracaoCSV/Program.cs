
using IntegracaoCSV.Core.UseCase;
using IntegracaoCSV.Infra;
using IntegracaoCSV.Infra.Repository;
using Microsoft.AspNetCore.Http.Features;

namespace IntegracaoCSV
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 10 * 1024 * 1024;
            });

            builder.Services.AddTransient<IIntegracaoCSVRepository, IntegracaoCSVRepository>();

            builder.Services.AddTransient<IntegraFilmesIndicados>();
            builder.Services.AddTransient<RetornaFilmesIndicados>();
            
            var app = builder.Build();

            DatabaseInitializer.InitializeDatabase();

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
