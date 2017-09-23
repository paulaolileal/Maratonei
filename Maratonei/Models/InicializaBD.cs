using Maratonei.Controllers.Components.Simplex;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Maratonei.Models {
    public static class InicializaBD {
        public static void Initialize( EntidadesContexto context ) {

            /*ObjectiveFunction of = new ObjectiveFunction( ObjectiveFunction.FuncType.Max );
            of.Add( "x1", 80 );
            of.Add( "x2", 60 );

            Restriction r1 = new Restriction( Restriction.FuncType.GreaterEqual);
            r1.Add( String.Empty, 24 );
            r1.Add( "x1", 4 );
            r1.Add( "x2", 6 );

            Restriction r2 = new Restriction( Restriction.FuncType.LessEqual );
            r2.Add( String.Empty, 16 );
            r2.Add( "x1", 4 );
            r2.Add( "x2", 2 );

            Restriction r3 = new Restriction( Restriction.FuncType.LessEqual );
            r3.Add( String.Empty, 3 );
            r3.Add( "x1", 0 );
            r3.Add( "x2", 1 );
            */
            /*
            ObjectiveFunction of = new ObjectiveFunction( ObjectiveFunction.FuncType.Min );
            of.Add( "x1", 1 );
            of.Add( "x2", 2 );

            Restriction r1 = new Restriction( Restriction.FuncType.GreaterEqual );
            r1.Add( String.Empty, 16 );
            r1.Add( "x1", 8 );
            r1.Add( "x2", 2 );

            Restriction r2 = new Restriction( Restriction.FuncType.LessEqual );
            r2.Add( String.Empty, 6 );
            r2.Add( "x1", 1 );
            r2.Add( "x2", 1 );

            Restriction r3 = new Restriction( Restriction.FuncType.GreaterEqual );
            r3.Add( String.Empty, 28 );
            r3.Add( "x1", 2 );
            r3.Add( "x2", 7 );

            List<Restriction> rest = new List<Restriction>( );
            rest.Add( r1 );
            rest.Add( r2 );
            rest.Add( r3 );
            */




            context.Database.EnsureCreated( );
            context.Usuarios.Add( new Usuario { Nome = "teste", Senha = "teste", traktUser = "teste" } );
            context.SaveChanges( );
        }
    }
}
