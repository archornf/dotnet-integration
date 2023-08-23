//using Stage;
//using Webpay.Integration.CSharp.Config;
//using Webpay.Integration.CSharp;
//using Webpay.Integration.CSharp.Order.Row;
//using Webpay.Integration.CSharp.Util.Testing;
//using Webpay.Integration.CSharp.WebpayWS;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using Stage.Models;
namespace SampleSite
{
    public static class Program
    {
        // If you have multiple entry points in the program: right-click project -> Properties -> General -> Startup object
        public static void Main(string[] args)
        {
            Console.WriteLine("Start");
            //MyConfigTest confTest = new MyConfigTest();
            //Webpay.Integration.CSharp.Config.IConfigurationProvider confTest = new MyConfigTest();
            //OrderTest orderTest = new OrderTest(confTest);
            //orderTest.TestOrder();

            // SampleSite
			// https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/adding-controller?view=aspnetcore-7.0&tabs=visual-studio
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                //ProductGenerator.Initialize(services);
            }
            host.Run();
        }

        // Along with using Microsoft.Extensions.Hosting, ConfigureWebHostDefaults also
        // require using Microsoft.AspNetCore.Hosting;
        // The project can't be a Console Application. ConfigureWebHostDefaults is for Web Application only.
        // So you can convert your Console Application into Web Applicaton by
        // replacing Sdk="Microsoft.NET.Sdk" with Sdk="Microsoft.NET.Sdk.Web" in your .csproj file as follows:
        // https://stackoverflow.com/questions/58097568/ihostbuilder-does-not-contain-a-definition-for-configurewebhostdefaults
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

//var builder = WebApplication.CreateBuilder(args);
//// Add services to the container.
//builder.Services.AddRazorPages();
//
//var app = builder.Build();
//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}
//
//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();
//app.UseAuthorization();
//app.MapRazorPages();
//app.Run();
