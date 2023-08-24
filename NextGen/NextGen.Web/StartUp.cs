
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NextGen.Contract.Domain;
using NextGen.Contract.Repository;
using NextGen.Domain;
using NextGen.Repository;

namespace NextGen.Web
{
    public class StartUp
    {
        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TaxCalculatorDbContext>((Action<DbContextOptionsBuilder>)(options => options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"))));
            services.AddScoped<IRateRepository, RateRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostalCodeRepository, PostalCodeRepository>();
            services.AddScoped<IUserDomain, UserDomain>();
            services.AddScoped<ITaxCalculationDomain, TaxCalculationDomain>();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints((Action<IEndpointRouteBuilder>)(endpoints => endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}")));
        }
    }
}
