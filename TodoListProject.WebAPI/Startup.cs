using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TodoListProject.Business.Abstract;
using TodoListProject.Business.Concrete;
using TodoListProject.Core.Caching;
using TodoListProject.Core.Caching.Microsoft;
using TodoListProject.DataAccess.Abstract;
using TodoListProject.DataAccess.Concrete.EntityFramework;

namespace TodoListProject.WebAPI
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
            services.AddScoped<ITodoItemService, TodoItemManager>();
            services.AddScoped<ITodoItemDal, EfTodoItemDal>();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();

            TodoContext.ConnectionString = Configuration.GetConnectionString("DefaultConnectionString");

            services.AddAutoMapper();
            services.AddMvc();
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
