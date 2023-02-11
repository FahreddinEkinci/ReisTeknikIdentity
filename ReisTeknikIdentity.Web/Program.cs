

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReisTeknikIdentity.Web.Data;
using ReisTeknikIdentity.Web.Extensions;
using ReisTeknikIdentity.Web.IdentityModels;
using ReisTeknikIdentity.Web.Localization;
using ReisTeknikIdentity.Web.Servicess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbConn")));

//builder.Services.AddIdentity<AppUser, AppRole>()
//    .AddEntityFrameworkStores<AppDbContext>()
//    .AddPasswordValidator<PasswordValidator>()
//    .AddErrorDescriber<LocalizationIdentityErrorDescripber>();

builder.Services.AddIdentityByExtensions();
builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EMailSetting")); // from appsettingsdevopment
builder.Services.Configure<SenderEmailSetting>(builder.Configuration.GetSection("SenderEmailSettingS")); // from appsettigns
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.ConfigureApplicationCookie(options =>
{

    var cookieBuilder = new CookieBuilder();

    cookieBuilder.Name = "ReisTeknikCookie";
    options.LoginPath = new PathString("/User/SignIn");
    options.LogoutPath = new PathString( "/User/SignOut");
    options.Cookie = cookieBuilder;
    options.ExpireTimeSpan = TimeSpan.FromDays(45);
options.SlidingExpiration = true;

});

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
    name: "siradan",
    pattern: "{controller}/{action}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();




