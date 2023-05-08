using CRUDApi.Models;
using EmployeeManagementAPI.Repository;
using EmployeeManagementAPI.Repository.Interface;
using EmployeeManagementAPI.Utilities;
using EmployeeManagementAPI.Utilities.File_Upload;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI
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
            services.AddDistributedMemoryCache();
            //register root upload path as file provider
            var fileProvider = new PhysicalFileProvider(Configuration.GetValue<string>("FileUpload:RootPath"));
            services.AddSingleton<IFileProvider>(fileProvider);
            // register file upload configurations as options
            foreach (FileUploadConfigTypeEnum config in Enum.GetValues(typeof(FileUploadConfigTypeEnum)))
            {
                services.Configure<FileUploadConfig>(config.ToString(),
                    Configuration.GetSection($"FileUpload:Config:{config}")
                );
            }

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeeManagementAPI", Version = "v1" });

            });
            services.AddDbContext<EmployeeApiDBContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:EmployeeDB"]));
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    a => a.WithOrigins("Domain Info"));
            });

            services.AddScoped<IUserLogin, UserLoginRepo>();
            services.AddScoped<IEmployeeData, EmployeeRepo>();
            services.AddScoped<IDepartmentData, DepartmentRepo>();
            services.AddScoped<IEmployeeDocumentData, DocumentRepo>();
            services.AddScoped<IValidationData, ValidationRepo>();
            //services.AddTransient<IJwtAuthData, AuthRepo>();

            services.AddMvc(options =>
            {
                // exclude `text/plain` output formatter
                options.OutputFormatters.RemoveType<StringOutputFormatter>();
                // register action filter for API exceptions 
                options.Filters.Add<APIResponseExceptionFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeManagementAPI v1"));
            }

            string uploadsDirectory = Path.Combine(env.ContentRootPath, "Uploads");
            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadsDirectory),
                RequestPath = "/Uploads"
            });
            
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(options =>
            {
                options.WithOrigins("Domain Information", "").AllowAnyMethod().AllowAnyHeader();
            });
            app.UseCors("CorsPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
          
        }
    }
}
