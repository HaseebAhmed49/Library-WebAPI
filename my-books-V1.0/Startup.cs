using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using my_books_V1._0.Data;
using my_books_V1._0.Data.Services;
using my_books_V1._0.Exceptions;

namespace my_books_V1._0
{
    public class Startup
    {
        public string ConnectionString { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("DefaultConnectionString");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            // configure DBContext with SQL
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConnectionString));

            // Configure the Services
            services.AddTransient<BooksService>();
            services.AddTransient<AuthorsService>();
            services.AddTransient<PublishersService>();
            services.AddTransient<LogsService>();


            services.AddApiVersioning(config=>
            {

                // Default version if version not specified
                config.DefaultApiVersion = new ApiVersion(1,0);
                config.AssumeDefaultVersionWhenUnspecified = true;


                // Http Header Based Versioning
                //config.ApiVersionReader = new HeaderApiVersionReader("custom-version-header");

                // Media Type API Versioning
                //config.ApiVersionReader = new MediaTypeApiVersionReader();


                //Now Query String versioning will be default
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2",new OpenApiInfo {Title="my-books-Haseeb", Version="v2" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c=> c.SwaggerEndpoint("/swagger/v2/swagger.json","my-books-UI_updated v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // Exception Handling
            app.ConfigureBuildInExceptionHandler(loggerFactory);
            //app.ConfigureCustomExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //AppDbInitializer.Seed(app);
        }
    }
}
