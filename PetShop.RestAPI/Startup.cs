using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PetShop.RestAPI.Helpers;
using PetShopCompulsory.Core.ApplicationService;
using PetShopCompulsory.Core.ApplicationService.implementation;
using PetShopCompulsory.Core.DomainService;
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

            //needs to be implemented when class is implemented
            JwtSecurityKey.SetSecret("a secret that needs to be at least 16 characters long");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //add cors
            services.AddCors();

            //Add JWT based authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidAudience = "https://localhost:24885:",
                        //if using external authentication provider (eg fb) ValidAudience = "Facebook.com"
                        ValidateIssuer = false, //also need this to be set to the above if using that
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = JwtSecurityKey.Key, //need to implement class JwtSecurityKey
                        ValidateLifetime = true, //validate expiration and not before values in token
                        ClockSkew = TimeSpan.FromMinutes(10) //10 min tolerance for expiration date?
                    };
                });

            

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

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

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

            app.UseHttpsRedirection(); // fra henrik, trying to turn off

            //cors must precede usemvc(), app.UseCors();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            //add_header 'Access-Control-Allow-Origin' 'http://localhost:4200' always;

            //use authentication 
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
