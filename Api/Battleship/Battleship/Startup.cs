using Battleship.Models.Auth;
using BattleshipApi.BattleField.BLL;
using BattleshipApi.BattleField.DAL;
using BattleshipApi.BattleField.DML.Interfaces;
using BattleshipApi.JWT.BLL;
using BattleshipApi.JWT.DML;
using BattleshipApi.JWT.DML.Interfaces;
using BattleshipApi.Match.BLL;
using BattleshipApi.Match.DAL;
using BattleshipApi.Match.DML.Interfaces;
using BattleshipApi.Player.BLL;
using BattleshipApi.Player.DAL;
using BattleshipApi.Player.DML.Interfaces;
using BattleshipApi.Ships.BLL;
using BattleshipApi.Ships.DAL;
using BattleshipApi.Ships.DML.Intefaces;
using BattleshipApi.SpecialPower.BLL;
using BattleshipApi.SpecialPower.DAL;
using BattleshipApi.SpecialPower.DML.Intefaces;
using BattleshipApi.Theme.BLL;
using BattleshipApi.Theme.DAL;
using BattleshipApi.Theme.DML.Interfaces;
using DataBaseHelper;
using DataBaseHelper.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

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

            services.AddSingleton<IDispatcherShips, DispatcherShips>();
            services.AddSingleton<IBoShips, BoShips>();

            services.AddSingleton<IDispatcherTheme, DispatcherTheme>();
            services.AddSingleton<IBoTheme, BoTheme>();

            ISigningConfigurations signingConfigurations = new SigningConfigurations();
            services.AddSingleton<ISigningConfigurations>(signingConfigurations);

            ITokenConfiguration tokenConfigurations = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<ITokenConfiguration>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddSingleton<IBoJWT, BoJWT>();

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddCors(options => {
                options.AddPolicy("mypolicy", builder => builder
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader());
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
                
                auth.AddPolicy("SUPERUSER", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .RequireClaim("SUPERUSER")
                    .Build());
            });
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
            app.UseCors("mypolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<WebSocketHandler>("/api/websocketHandler");
            });
        }
    }
}
