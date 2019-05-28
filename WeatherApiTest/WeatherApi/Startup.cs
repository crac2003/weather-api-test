using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Weather.Domain.Http;
using Weather.Domain.Services;
using Weather.Service.OpenWeather;

namespace WeatherApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IWeatherService, WeatherService>();
            services.AddSingleton<IBackupService, LocalBackupService>();
            services.AddSingleton<IFileSystemService, FileSystemService>();
            services.AddSingleton<IWeatherHttpClient, WeatherHttpClient>();

            services.AddSingleton<IWeatherDataProvider, OpenWeatherProvider>();

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true);
            var config = configBuilder.Build();
            services.AddSingleton<IOpenWeatherConfig>(config.GetSection("OpenWeather").Get<OpenWeatherConfig>());
            services.AddSingleton<ILocalBackupConfig>(config.GetSection("ApplicationSettings").Get<BackUpConfig>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

    public class BackUpConfig : ILocalBackupConfig
    {
        public string BackupFolder { get; set; }
    }

    public class OpenWeatherConfig : IOpenWeatherConfig
    {
        public string OpenWeatherUrl { get; set; }
        public string OpenWeatherKey { get; set; }
    }
}
