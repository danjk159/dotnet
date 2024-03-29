using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TodoApi.Models;

namespace TodoApi {
    public class Startup {
        private readonly ILogger _logger;
        public Startup (IConfiguration configuration, ILogger<Startup> logger) {
            Configuration = configuration;
            _logger = logger;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices (IServiceCollection services) {
            //mysql配置
            // var connection = Configuration.GetConnectionString ("MysqlConnection");
            // services.AddDbContext<TodoContext> (options => options.UseMySql (connection));
            //sqlite配置
            services.AddDbContext<TodoContext> (options => options.UseSqlite ("Data Source=test.db"));
            services.AddControllers ();
            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine (AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments (xmlPath);
            });
            _logger.LogInformation ("Added TodoRepository to services");
        }

        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                _logger.LogInformation ("In Development environment");
                app.UseDeveloperExceptionPage ();
            }
            app.UseSwagger ();
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseHttpsRedirection ();
            app.UseRouting ();
            app.UseAuthorization ();
            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}