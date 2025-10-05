using Api_InfoTransito.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddApiConfiguration()
    .ResolveDependencies();

var app = builder.Build();

app.UseApiConfiguration();

app.Run();
