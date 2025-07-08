using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Services;
using ShowTime.Components;
using ShowTime.DataAccess;
using ShowTime.DataAccess.GenericInterface;
using ShowTime.DataAccess.GenericRepository;
using ShowTime.DataAccess.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth-token";
        options.LoginPath = "/login";
        //options.AccessDeniedPath = "/access-denied";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("SHOWTIME_CONNECTION_STRING");
builder.Services.AddDbContext<ShowTimeContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddTransient<IGenericRepository<Artists>, GenericRepository<Artists>>();
builder.Services.AddTransient<IArtistsService, ArtistService>();

builder.Services.AddTransient<IGenericRepository<Festival>, GenericRepository<Festival>>();
builder.Services.AddTransient<IFestivalService, FestivalService>();

builder.Services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<IGenericRepository<Lineup>, GenericRepository<Lineup>>();
builder.Services.AddTransient<ILineupService, LineupService>();

builder.Services.AddTransient<IGenericRepository<Booking>, GenericRepository<Booking>>();
builder.Services.AddTransient<IBookingService, BookingService>();

builder.Services.AddTransient<IGenericRepository<Ticket>, GenericRepository<Ticket>>();
builder.Services.AddTransient<ITicketService, TicketService>();


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
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(ShowTime.Client._Imports).Assembly);



app.Run();
