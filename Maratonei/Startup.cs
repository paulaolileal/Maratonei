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
using OPTANO.Modeling.Optimization;
using OPTANO.Modeling.Optimization.Solver.GLPK;
using System.IO;
using Newtonsoft.Json;

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
            /*
            GLPKInput teste = new GLPKInput( );

            teste.Variables.Add( "x" );
            teste.Variables.Add( "y" );

            GLPKRestriction r1 = new GLPKRestriction( );
            r1.Values.Add( 10 );
            r1.Values.Add( 20 );
            r1.Operation = GLPKRestriction.Operator.GreaterOrEqual;
            r1.Disponibility = 2;

            GLPKRestriction r2 = new GLPKRestriction( );
            r2.Values.Add( 40 );
            r2.Values.Add( 60 );
            r2.Operation = GLPKRestriction.Operator.GreaterOrEqual;
            r2.Disponibility = 64;

            GLPKRestriction r3 = new GLPKRestriction( );
            r3.Values.Add( 50 );
            r3.Values.Add( 20 );
            r3.Operation = GLPKRestriction.Operator.GreaterOrEqual;
            r3.Disponibility = 34;

            teste.Restrictions.Add( r1 );
            teste.Restrictions.Add( r2 );
            teste.Restrictions.Add( r3 );

            teste.Objective.Values.Add( 0.6 );
            teste.Objective.Values.Add( 0.8 );

            var hy = JsonConvert.SerializeObject( teste );
            Console.Write( hy );*/

            //string path = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User) + ";" + Directory.GetCurrentDirectory();
            //Environment.SetEnvironmentVariable("Path", path, EnvironmentVariableTarget.User);

            //var model = new Model();
            //var x = new Variable("x");
            //var y = new Variable("y");
            //model.AddConstraint(10 * x + 20 * y >= 2);
            //model.AddConstraint(40 * x + 60 * y >= 64);
            //model.AddConstraint(50 * x + 20 * y >= 34);
            //model.AddObjective(new Objective(0.6 * x + 0.8 * y));

            //var solver = new GLPKSolver();
            //solver.Solve(model);
            //var solution = solver.Solve(model);


            InicializaBD.Initialize( contexto );
        }
    }
}
