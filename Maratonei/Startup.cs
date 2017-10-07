using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Maratonei.Models;

namespace Maratonei {
    public class Startup {
        public Startup( IHostingEnvironment env ) {
            var builder = new ConfigurationBuilder( )
                .SetBasePath( env.ContentRootPath )
                .AddJsonFile( "appsettings.json", optional: false, reloadOnChange: true )
                .AddJsonFile( $"appsettings.{env.EnvironmentName}.json", optional: true )
                .AddEnvironmentVariables( );
            Configuration = builder.Build( );
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services ) {
            var sqlConnectionString = Configuration.GetConnectionString( "DataAccessMySqlProvider" );

            services.AddDbContext<EntidadesContexto>( options =>
                 options.UseMySql(
                     sqlConnectionString,
                     b => b.MigrationsAssembly( "AspNetCoreMultipleProject" )
                 )
            );

            // Add framework services.
            services.AddMvc( );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, EntidadesContexto contexto ) {
            loggerFactory.AddConsole( Configuration.GetSection( "Logging" ) );
            loggerFactory.AddDebug( );

            if (env.IsDevelopment( )) {
                app.UseDeveloperExceptionPage( );
                app.UseBrowserLink( );
            } else {
                app.UseExceptionHandler( "/Home/Error" );
            }

            app.UseStaticFiles( );

            app.UseMvc( routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}" );
            } );

            InicializaBD.Initialize( contexto );
        }
    }
}
