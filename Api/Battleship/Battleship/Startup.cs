using BattleshipApi.BattleField.BLL;
using BattleshipApi.BattleField.DAL;
using BattleshipApi.BattleField.DML.Interfaces;
using BattleshipApi.Match.BLL;
using BattleshipApi.Match.DAL;
using BattleshipApi.Match.DML.Interfaces;
using BattleshipApi.Player.BLL;
using BattleshipApi.Player.DAL;
using BattleshipApi.Player.DML.Interfaces;
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
            services.AddCors(options => 
                options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            services.AddControllers();

            services.AddSignalR();
            services.AddSingleton<WebSocketConnections>();

            services.AddTransient<IUnitOfWork>(unit => new UnitOfWork(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<IDispatcherPlayer, DispatcherPlayer>();
            services.AddSingleton<IBoPlayer, BoPlayer>();

            services.AddSingleton<IDispatcherMatch, DispatcherMatch>();
            services.AddSingleton<IBoMatch, BoMatch>();

            services.AddSingleton<IDispatcherBattleField, DispatcherBattleField>();
            services.AddSingleton<IBoBattleField, BoBattleField>();

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

            app.UseReactStaticFiles();

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<WebSocketHandler>("/api/websocketHandler");
            });
        }
    }
}
