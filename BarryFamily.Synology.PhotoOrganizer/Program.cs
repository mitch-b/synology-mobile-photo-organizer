// See https://aka.ms/new-console-template for more information

using BarryFamily.Synology.PhotoOrganizer;
using BarryFamily.Synology.PhotoOrganizer.Models.Configuration;
using BarryFamily.Synology.PhotoOrganizer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<PhotoOrganizerWorker>();

        services.AddOptions<SynologyConnection>().BindConfiguration("SynologyConnection");
        services.AddOptions<OrganizeInfo>().BindConfiguration("OrganizeInfo");

        services.AddScoped<ISynologyFileService, SynologyFileService>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<ITokenizedFilePathService, TokenizedFilePathService>();
    })
    .ConfigureAppConfiguration((hostContext, configBuilder) =>
    {
        configBuilder.AddUserSecrets<Program>();
    });

using var host = builder.Build();

await host.RunAsync();
