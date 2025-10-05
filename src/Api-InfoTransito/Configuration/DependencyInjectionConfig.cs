using Business_InfoTransito.Interfaces;
using Business_InfoTransito.Interfaces.IRepositories.Events;
using Business_InfoTransito.Interfaces.IRepositories.Location;
using Business_InfoTransito.Interfaces.IRepositories.People;
using Business_InfoTransito.Interfaces.IServices.Events;
using Business_InfoTransito.Interfaces.IServices.People;
using Business_InfoTransito.Notifications;
using Business_InfoTransito.Services.Events;
using Business_InfoTransito.Services.People;
using Data_InfoTransito.Context;
using Data_InfoTransito.Repository.Events;
using Data_InfoTransito.Repository.Location;
using Data_InfoTransito.Repository.People;

namespace Api_InfoTransito.Configuration;

public static class DependencyInjectionConfig
{
    public static WebApplicationBuilder ResolveDependencies(this WebApplicationBuilder builder)
    {
        //Project dependences
        builder.Services.AddScoped<InfoTransitoDbContext>();
        builder.Services.AddScoped<IPersonAddressRepository, PersonAddressRepository>();
        builder.Services.AddScoped<ISinistroAddressRepository, SinistroAddressRepository>();
        builder.Services.AddScoped<IPersonRepository, PersonRepository>();
        builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
        builder.Services.AddScoped<ISinistroRepository, SinistroRepository>();

        builder.Services.AddScoped<INotifier, Notifier>();
        builder.Services.AddScoped<IPersonService, PersonService>();
        builder.Services.AddScoped<IVehicleService, VehicleService>();
        builder.Services.AddScoped<ISinistroService, SinistroService>();

        return builder;
    }
}
