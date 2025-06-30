using Microsoft.EntityFrameworkCore;
using ShowTime.Client.Pages;
using ShowTime.Components;
using ShowTime.DataAccess;
using ShowTime.DataAccess.GenericInterface;
using ShowTime.DataAccess.GenericRepository;
using ShowTime.DataAccess.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

var connectionString = builder.Configuration.GetConnectionString("SHOWTIME_CONNECTION_STRING");
builder.Services.AddDbContext<ShowTimeContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddTransient<IGenericRepository<Artists>, GenericRepository<Artists>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ShowTime.Client._Imports).Assembly);

app.Run();
