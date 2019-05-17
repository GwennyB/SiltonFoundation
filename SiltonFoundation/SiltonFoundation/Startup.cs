using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SiltonFoundation.Data;
using SiltonFoundation.Models;
using SiltonFoundation.Models.Interfaces;
using SiltonFoundation.Models.Services;

namespace SiltonFoundation
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppUserDbContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<AppUserDbContext>(options =>
                options.UseSqlServer($"Data Source={Configuration["RDS_HOSTNAME"]};Initial Catalog={Configuration["RDS_DBNAME_USER"]};User ID={Configuration["RDS_USERNAME"]};Password={Configuration["RDS_PASSWORD"]}"));

            services.AddDbContext<ScholarshipDbContext>(options =>
                options.UseSqlServer($"Data Source={Configuration["RDS_HOSTNAME"]};Initial Catalog={Configuration["RDS_DBNAME_SCHOL"]};User ID={Configuration["RDS_USERNAME"]};Password={Configuration["RDS_PASSWORD"]}"));


            services.AddMvc();

            services.AddScoped<ISFAppManager, SFAppMgmtSvc>();
            services.AddScoped<IEvaluationManager, EvaluationMgmtSvc>();
            services.AddScoped<IAppResponseManager, AppResponseMgmtSvc>();
            services.AddScoped<IQuestionManager, QuestionMgmtSvc>();
            services.AddScoped<IAppUserManager, AppUserMgmtSvc>();

        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            // TODO: assign error redirect route
            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseMvc(route =>
            {
                route.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }


    }
}
