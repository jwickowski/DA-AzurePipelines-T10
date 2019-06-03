using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Core;
using ToDoList.EntityFramework;
using ToDoList.Storage.Fake;
using ToDoList.WebApi.Controllers;

namespace ToDoList.WebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<ListItemReader, ListItemReader>();
            services.AddTransient<ListItemCreator, ListItemCreator>();
            services.AddTransient<ListItemChecker, ListItemChecker>();


//                var connectionString = Configuration.GetConnectionString("ToDoListDatabase");
//                services.AddDbContext<ToDoListContext>(options =>
//                    options.UseSqlServer(connectionString));
//
//                var dbStorage = new FakeStorage();
//                services.AddTransient<IListItemStorageSaver, FakeStorage>((serviceCollection) => dbStorage);
//                services.AddTransient<IListItemStorageReader, FakeStorage>((serviceCollection) => dbStorage);
//                services.AddTransient<IListItemStorageChanger, FakeStorage>((serviceCollection) => dbStorage);

            var fakeStorage = new FakeStorage();
            services.AddSingleton<IListItemStorageSaver, FakeStorage>((serviceCollection) => fakeStorage);
            services.AddSingleton<IListItemStorageReader, FakeStorage>((serviceCollection) => fakeStorage);
            services.AddSingleton<IListItemStorageChanger, FakeStorage>((serviceCollection) => fakeStorage);

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env /*, ToDoListContext context */)
        {
//            if (useRealDb)
//            {
//                context.Database.Migrate();
//            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");

            app.UseMvc();


        }
    }
}