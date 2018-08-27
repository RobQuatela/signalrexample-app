using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using signlarexample.Hubs;

namespace signlarexample
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
            Action<CorsOptions> options = SetUp;
            services.AddCors(options);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSignalR();
        }

        public void SetUp(CorsOptions options)
        {
            Action<CorsPolicyBuilder> builder = confiurePolicy;
            options.AddPolicy("CorsPolicy", builder);
        }

        public void confiurePolicy(CorsPolicyBuilder builder)
        {
            builder.AllowAnyMethod().AllowAnyHeader()
                    .WithOrigins("http://localhost:4200")
                    .AllowCredentials();
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
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseSignalR(routes => {
                routes.MapHub<MessageHub>("/messagehub");
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
