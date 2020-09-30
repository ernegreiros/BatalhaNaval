using BattleshipApi.SpecialPower.BLL;
using BattleshipApi.SpecialPower.DAL;
using BattleshipApi.SpecialPower.DML.Intefaces;
using DataBaseHelper;
using DataBaseHelper.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Battleship
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
            services.AddSignalR();
            services.AddSingleton<WebSocketConnections>();
            services.AddSingleton<IUnitOfWork>(new UnitOfWork(Configuration["ConnectionString"]));
            
            services.AddSingleton<IDispatcherSpecialPower, DispatcherSpecialPower>();
            services.AddSingleton<IBoSpecialPower, BoSpecialPower>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<WebSocketHandler>("/websocketHandler");
            });
        }
    }
}
