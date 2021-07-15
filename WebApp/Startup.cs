using System;
using System.IO;
using System.Reflection;
using Core.BLL;
using Core.BLL.Helpers;
using Core.DAL;
using Core.DAL.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using ServiceProvider = Core.BLL.Helpers.ServiceProvider;
#pragma warning disable 1591

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private const string CorsPolicy = "CorsAllowAll";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("MSSqlConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddSingleton<RepositoryFactory>();
            services.AddScoped<RepositoryProvider>();
            services.AddScoped<UnitOfWork>();
            
            services.AddSingleton<ServiceFactory>();
            services.AddScoped<ServiceProvider>();
            services.AddScoped<AppBLL>();

            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy, builder =>
                {
                    builder
                        .WithOrigins("http://localhost:8080")
                        .WithHeaders(
                            "cache",
                            HeaderNames.Accept,
                            HeaderNames.Authorization,
                            HeaderNames.ContentType,
                            HeaderNames.Origin)
                        .WithMethods("OPTIONS", "GET", "POST", "PUT", "DELETE");
                });
            });

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PostOffice API", Version = "v1", 
                    Description = "Source code: https://github.com/hankur/PostOfficeDemo"
                });
                
                // include xml comments (enable creation in csproj file)
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Auto-generate database tables
            // (c) https://stackoverflow.com/a/42356110
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope())
            {
                var context = serviceScope?.ServiceProvider.GetRequiredService<AppDbContext>();
                context?.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(CorsPolicy);
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}