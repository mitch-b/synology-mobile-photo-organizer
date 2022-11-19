// See https://aka.ms/new-console-template for more information

using BarryFamily.Synology.PhotoOrganizer;
using BarryFamily.Synology.PhotoOrganizer.Models.Configuration;
using BarryFamily.Synology.PhotoOrganizer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(
    (services) =>
    {
        services.AddHostedService<PhotoOrganizerWorker>();
        services.AddOptions<SynologyConnection>();
        services.AddScoped<ITokenizedFilePathService, TokenizedFilePathService>();
    });

using var host = builder.Build();

await host.RunAsync();
