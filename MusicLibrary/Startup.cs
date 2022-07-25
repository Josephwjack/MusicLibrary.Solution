using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MusicLibrary 
{
  public class Startup
  {
    //This constructor will create an iteration of the Startup class that contains specific settings and variables to run our project 
    public Startup(IWebHostEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddEnvironmentVariables();
      Configuration = builder.Build();
    }
    //This is part of adding custom configurations to our project
    public IConfigurationRoot Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      //Adds MVC service to project
      services.AddMvc();
    }
    //responsible for telling our app how to handle requests to the server
    public void Configure(IApplicationBuilder app)
    {

      app.UseDeveloperExceptionPage(); 
      
      app.UseRouting();
      app.UseStaticFiles();
      app.UseEndpoints(routes =>
      {
        routes.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
      });

      app.Run(async (context) =>
      {
        await context.Response.WriteAsync("Hello World!");
      });
    }
  }
  public static class DBConfiguration
  {
    public static string ConnectionString = "server=localhost;user id=root;password=epicodus;port=3306;database=music_library;";
  }
}