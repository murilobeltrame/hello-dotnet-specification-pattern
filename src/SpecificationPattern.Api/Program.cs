using Microsoft.EntityFrameworkCore;
using SpecificationPattern.Application.Interfaces;
using SpecificationPattern.Application.UseCases;
using SpecificationPattern.Domain.Entities;
using SpecificationPattern.Domain.Interfaces;
using SpecificationPattern.Infra.Repositories;

namespace SpecificationPattern.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("ApplicationContext");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString));
            builder.Services.AddTransient<IRepository<Country>, Repository<Country>>();
            builder.Services.AddTransient<IRepository<Grape>, Repository<Grape>>();
            builder.Services.AddTransient<IRepository<Region>, Repository<Region>>();
            builder.Services.AddTransient<IRepository<Wine>, Repository<Wine>>();
            builder.Services.AddTransient<IRepository<Winery>, Repository<Winery>>();
            builder.Services.AddTransient<ICountryUseCases, CountryUseCases>();
            builder.Services.AddTransient<IRegionUseCases, RegionUseCases>();
            builder.Services.AddTransient<IWineUseCases, WineUseCases>();
            builder.Services.AddTransient<IWineryUseCases, WineryUseCases>();


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}