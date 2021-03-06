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
using BattleshipApi.MatchAttacks.BLL;
using BattleshipApi.MatchAttacks.DAL;
using BattleshipApi.MatchAttacks.DML.Interfaces;
using BattleshipApi.MatchSpecialPower.BLL;
using BattleshipApi.MatchSpecialPower.DAL;
using BattleshipApi.MatchSpecialPower.DML.Interfaces;
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
            services.AddCors(options => options.AddDefaultPolicy(
                                        builder => builder.SetIsOriginAllowed(x => true)
                                                          .AllowAnyMethod()
                                                          .AllowAnyHeader()
                                                          .AllowCredentials()));
            services.AddControllers();

            services.AddSignalR();
            services.AddSingleton<WebSocketConnections>();

            services.AddScoped<IUnitOfWork>(unit => new UnitOfWork(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDispatcherPlayer, DispatcherPlayer>();
            services.AddScoped<IBoPlayer, BoPlayer>();

            services.AddScoped<IDispatcherMatch, DispatcherMatch>();
            services.AddScoped<IBoMatch, BoMatch>();

            services.AddScoped<IDispatcherBattleField, DispatcherBattleField>();
            services.AddScoped<IBoBattleField, BoBattleField>();

            services.AddScoped<IDispatcherSpecialPower, DispatcherSpecialPower>();
            services.AddScoped<IBoSpecialPower, BoSpecialPower>();

            services.AddScoped<IDispatcherShips, DispatcherShips>();
            services.AddScoped<IBoShips, BoShips>();

            services.AddScoped<IDispatcherTheme, DispatcherTheme>();
            services.AddScoped<IBoTheme, BoTheme>();

            services.AddScoped<IDispatcherMatchSpecialPower, DispatcherMatchSpecialPower>();
            services.AddScoped<IBoMatchSpecialPower, BoMatchSpecialPower>();

            services.AddScoped<IBoMatchAttacks, BoMatchAttacks>();
            services.AddScoped<IDispatcherMatchAttacks, DispatcherMatchAttacks>();

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

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<WebSocketHandler>("/websocketHandler");
                endpoints.MapControllers();
            });
        }
    }
}
