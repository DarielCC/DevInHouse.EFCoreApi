using DevInHouse.EFCoreApi.Api.Configuraoes;
using DevInHouse.EFCoreApi.Core.Interfaces;
using DevInHouse.EFCoreApi.Core.Services;
using DevInHouse.EFCoreApi.Data.Context;
using DevInHouse.EFCoreApi.Data.Repositories;
using DevInHouse.EFCoreApi.Domain.Interfaces;
using DevInHouse.EFCoreApi.Domain.Notifications;
using DevInHouse.EFCoreApi.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("livros", new OpenApiInfo()
    {
        Title = "Livraria Livros",
        Description = "API Livraria",
        Version = "livros"
    });
    options.SwaggerDoc("autores", new OpenApiInfo()
    {
        Title = "Livraria Autores",
        Description = "API Livraria",
        Version = "autores"
    });
    options.SwaggerDoc("autoresv2", new OpenApiInfo()
    {
        Title = "Livraria Autores V2",
        Description = "API Livraria",
        Version = "autoresv2"
    });
});

builder.Services.AddMvc(options =>
{
    options.Filters.Add<NotificacaoFilter>();
    options.Filters.Add<ModelStateFilter>();
});

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<ILivroService, LivroService>();
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<INotificacaoService, NotificacaoService>();

builder.Services.Configure<RouteOptions>(
        options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });

builder.Services.AddApiVersioning(config => {
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(
    options => {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/livros/swagger.json", "Documentação livros");
        options.SwaggerEndpoint("/swagger/autores/swagger.json", "Documentação autores");
        options.SwaggerEndpoint("/swagger/autoresv2/swagger.json", "Documentação autores V2");
    });
}

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseHsts();

app.UseHttpsRedirection();
app.UseOnExceptionHandler(app.Environment);

app.UseAuthorization();

app.MapControllers();

app.Run();
