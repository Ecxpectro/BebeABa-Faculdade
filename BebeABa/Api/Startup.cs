using Api.Business;
using Api.Business.Interfaces;
using Api.Repository;
using Api.Repository.Interfaces;
using DB.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Api
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                
            });

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChildrenRepository, ChildrenRepository>();
            services.AddScoped<IChildrenTimeLineRepository, ChildrenTimeLineRepository>();
            services.AddScoped<IMainForumRepository, MainForumRepository>();
            services.AddScoped<IForumAnswerRepository, ForumAnswerRepository>();


            //Business
            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<IChildrenBusiness, ChildrenBusiness>();
            services.AddScoped<IChildrenTimeLineBusiness, ChildrenTimeLineBusiness>();
            services.AddScoped<IMainForumBusiness, MainForumBusiness>();
            services.AddScoped<IForumAnswerBusiness, ForumAnswerBusiness>();


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<BebeaBaContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("Connection"), sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                    sqlOptions.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
                    sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                }).EnableSensitiveDataLogging();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
