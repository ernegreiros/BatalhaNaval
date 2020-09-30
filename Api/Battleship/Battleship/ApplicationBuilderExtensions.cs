using Microsoft.AspNetCore.Builder;
using System.IO;

namespace Battleship
{
    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder UseReactStaticFiles(this IApplicationBuilder builder)
        {
            builder.Use(async (context, next) =>
            {
                await next();
                var path = context.Request.Path.Value;

                if (!path.StartsWith("/api") && !Path.HasExtension(path))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });
            return builder;
        }
    }
}
