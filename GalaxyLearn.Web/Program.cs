using GalaxyLearn.Core.Senders;
using GalaxyLearn.Core.Services;
using GalaxyLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<FormOptions>(options => { options.MultipartBodyLengthLimit = 52428800; });

#region Database Context

builder.Services.AddDbContext<GalaxyLearn.DataLayer.Context.GalaxyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("GalaxyCnn"));
});

#endregion

#region IoC

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISmsSevice, SmsService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IUserPanelService, UserPanelService>();
builder.Services.AddTransient<IWalletService, WalletService>();
builder.Services.AddTransient<IUserAdminPanelService, UserAdminPanelService>();
builder.Services.AddTransient<IPermissionService, PermissionService>();
builder.Services.AddTransient<ICourseService, CourseService>();

#endregion

builder.Services.AddAutoMapper(typeof(Program).Assembly);

#region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromDays(10);
});

#endregion

builder.Services.AddRazorPages();

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
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
