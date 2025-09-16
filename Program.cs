using HospitalDeVehiculosUltimaVersion.Factory;
using HospitalDeVehiculosUltimaVersion.Factory.FactoryPago;
using HospitalDeVehiculosUltimaVersion.Factory.FactoryRoll;
using HospitalDeVehiculosUltimaVersion.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<HospitalDeVehiculosContext>(options =>
options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

//jorge
builder.Services.AddScoped<ServicioPagoDeQr>();
builder.Services.AddScoped<ServicioPagoDeTarjeta>();

// DI de tus implementaciones
builder.Services.AddScoped<IRoleResolver, EfRoleResolver>();
builder.Services.AddScoped<ICurrentUserSession, HttpSessionUser>();

// Necesario para HttpSessionUser
builder.Services.AddHttpContextAccessor();

// ?? Session súper simple
builder.Services.AddSession(o =>
{
    o.IdleTimeout = TimeSpan.FromMinutes(60);
    o.Cookie.Name = "hv_sid";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
//Habilitar session ANTES de MapRazorPages
app.UseSession();

app.MapRazorPages();

app.Run();
