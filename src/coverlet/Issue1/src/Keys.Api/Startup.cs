using System;
using System.Diagnostics.CodeAnalysis;
using Keys.Application.Services;
using Keys.Application.Services.Contracts;
using Keys.Data.Context;
using Keys.Data.Context.Contracts;
using Keys.Data.Factories;
using Keys.Data.Factories.Contracts;
using Keys.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Keys.Data.Repositories.Contracts;

namespace Keys.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddScoped<IDataContext, DataContext>();

            services.AddScoped<IKeyRepository, KeyRepository>();
            services.AddScoped<IKeyProvider, KeyProvider>();
    
            services.AddScoped<IKeyFactory, KeyFactory>();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly("Keys.Data");
                        sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDataContext datacontext, ILoggerFactory loggerFactory)
        {
            if (!datacontext.Database.IsInMemory())
            {
                datacontext.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
