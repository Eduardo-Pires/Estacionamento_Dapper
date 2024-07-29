using System.Data;
using MySql.Data.MySqlClient;
using estacionamento.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Registrar IDbConnection com a string de conexão
builder.Services.AddTransient<IDbConnection>((sp) => 
    new MySqlConnection(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped(typeof(IRepositorio<>), typeof(RepositorioDapper<>));

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
