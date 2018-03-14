using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using recipeservice.Services.Interfaces;
using recipeservice.Services;
using recipeservice.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.HttpOverrides;

namespace recipeservice
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                }));

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IThingService, ThingService>();
            services.AddSingleton<IThingGroupService, ThingGroupService>();
            services.AddSingleton<ITagsService, TagService>();
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseNpgsql(Configuration.GetConnectionString("RecipeDb")));
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IExtraAttributeTypeService, ExtraAttributeTypeService>();
            services.AddTransient<IPhaseService, PhaseService>();
            services.AddTransient<IPhaseParameterService, PhaseParameterService>();
            services.AddTransient<IPhaseProductService, PhaseProductService>();
            services.AddTransient<IRecipeService, RecipeService>();
            services.AddTransient<IRecipePhaseService, RecipePhaseService>();
            services.AddTransient<IRecipeAutomaticService,RecipeAutomaticService>();
            services.AddResponseCaching();
            services.AddMvc((options) =>
            {
                options.CacheProfiles.Add("recipecache", new CacheProfile()
                {
                    Duration = Convert.ToInt32(Configuration["CacheDuration"]),
                    Location = ResponseCacheLocation.Any
                });
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCaching();
            app.UseCors("CorsPolicy");
            app.UseForwardedHeaders (new ForwardedHeadersOptions {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
