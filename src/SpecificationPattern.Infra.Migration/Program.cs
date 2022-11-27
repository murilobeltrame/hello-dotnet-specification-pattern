// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpecificationPattern.Domain.Entities;
using SpecificationPattern.Infra.Repositories;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration
            .GetSection("ConnectionStrings:ApplicationContext").Value;
        services.AddDbContext<ApplicationContext>(options =>
            options.UseNpgsql(connectionString, o =>
                o.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)));
    })
    .Build();

await host.RunAsync();