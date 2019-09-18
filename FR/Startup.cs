using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FR.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FR.Api.Services;
using FR.Domain.Interfaces;
using FR.Domain.Models;
using FR.Infrastructure.Repositories;

namespace FR.Api
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
            services.AddDbContext<TournamentDbContenxt>(options => options.UseSqlite("Data Source=tournament.db"));
            services.AddScoped(typeof(IRepository<Group>), typeof(Repository<Group>));
            services.AddScoped(typeof(IRepository<Result>), typeof(Repository<Result>));
            services.AddScoped(typeof(GroupService), typeof(GroupService));
            services.AddScoped(typeof(ResultsService), typeof(ResultsService));
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
}
