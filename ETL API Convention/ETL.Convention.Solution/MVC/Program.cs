using Microsoft.AspNetCore.Diagnostics;
using MVC.Data;
using MVC.Handler;
using MVC.HttpServices;
using System.Globalization;
using Utilities.Constants;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
              .AddJsonFile(GeneralConstants.JsonFileName).Build();

//All http service call here
builder.Services.AddScoped<ReportDbContext>();
builder.Services.AddHttpClient<CategorysHttpService>();
builder.Services.AddHttpClient<ProductsHttpService>();
builder.Services.AddControllersWithViews();

//Set the global date format dd/mm/yyyy.
CultureInfo cultureInfo = CultureInfo.GetCultureInfo("en-GB");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.CurrentCulture = cultureInfo;

//cookies save this browser
builder.Services.AddMvc();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthentication("Cookies")
                   .AddCookie("Cookies", config =>
                   {
                       config.Cookie.Name = "__SCinfo__";
                       config.LoginPath = "/";
                       config.SlidingExpiration = true;
                   });

builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(240); });
// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.UseSession();

//app.UseExceptionHandler(errorApp =>
//{
//    errorApp.Run(context =>
//    {
//        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

//        // Call the custom exception handling logic using the delegate
//        var exception = exceptionHandlerPathFeature?.Error;
//        ExceptionHandlingHelper.HandleException(errorApp, exception);

//        return Task.CompletedTask;
//    });
//});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
