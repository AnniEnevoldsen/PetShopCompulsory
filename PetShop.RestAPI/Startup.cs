using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PetShopCompulsory.Core.ApplicationService;
using PetShopCompulsory.Core.ApplicationService.implementation;
using PetShopCompulsory.Core.DomainService;
using PetShopCompulsory.Core.Entities;
using PetShopCompulsory.Core.ServiceFolder;
using PetShopCompulsory.Core.ServiceFolder.implementation;
using PetShopCompulsory.Infrastructure.Data;
using PetShopCompulsory.Infrastructure.Data.SQLRepositories;

namespace PetShop.RestAPI
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        private IConfiguration _conf { get; }

        private IHostingEnvironment _env { get; set; }

       // public IConfiguration Configuration { get; }


        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _conf = builder.Build();
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsDevelopment())
            {
                services.AddDbContext<PetShopAppContext>(
                    opt => opt.UseSqlite("Data Source=petShopApp.db"));
            }
            else if (_env.IsProduction())
            {
                services.AddDbContext<PetShopAppContext>(
                    opt => opt
                        .UseSqlServer(_conf.GetConnectionString("DefaultConnection")));
            }

            ////services.AddDbContext<PetShopAppContext>(opt => opt.UseInMemoryDatabase("DbOne"));
            //services.AddDbContext<PetShopAppContext>(opt => opt.UseSqlite("Data source=petShopApp.db"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<IPetRepository, sqlPetRepository>();
            services.AddScoped<IPetService, PetService>();

            services.AddScoped<IOwnerRepository, sqlOwnerRepository>();
            services.AddScoped<IOwnerService, OwnerService>();

            //below is so we don't get a never ending loop(might not be relevant)
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<PetShopAppContext>();
                    DBInitializer.SeedingDB(ctx);
                }
            }
            else
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<PetShopAppContext>();
                    ctx.Database.EnsureCreated();
                }
                app.UseHsts();
            }
            app.UseMvc();
        }
    }
}
