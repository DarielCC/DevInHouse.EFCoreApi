using AutoMapper;
using DevInHouse.EFCoreApi.Application.ApplicationServices;
using DevInHouse.EFCoreApi.Application.AutoMapper;
using DevInHouse.EFCoreApi.Core.Interfaces;
using DevInHouse.EFCoreApi.Core.Services;
using DevInHouse.EFCoreApi.Data.Context;
using DevInHouse.EFCoreApi.Data.Repositories;
using DevInHouse.EFCoreApi.Domain.Interfaces;
using DevInHouse.EFCoreApi.Domain.Notifications;
using DevInHouse.EFCoreApi.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
                    {
                        options.Password.RequiredLength = 6;
                        options.Password.RequiredUniqueChars = 3;
                        options.Password.RequireNonAlphanumeric = false;
                    }
                )
                .AddEntityFrameworkStores<DataContext>()
                .AddUserManager<UserManager<IdentityUser>>()
                .AddRoleManager<RoleManager<IdentityRole>>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 3;
    options.Password.RequireNonAlphanumeric = false;
});
builder.Services.AddControllersWithViews();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new DomainToViewModel());
    mc.AddProfile(new ViewModelToDomain());
});

var mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUsuarioApplicationService, UsuarioApplicationService>();
builder.Services.AddScoped<IAutorApplicationService, AutorApplicationService>();
builder.Services.AddScoped<ILivroApplicationService, LivroApplicationService>();
builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<ILivroService, LivroService>();
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<INotificacaoService, NotificacaoService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
