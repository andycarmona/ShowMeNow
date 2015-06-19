// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthContext.cs" company="Uni-app">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ShowMeNow.API
{
    using System.Data.Entity;

    using AngularJSAuthentication.API.Entities;

    using Microsoft.AspNet.Identity.EntityFramework;

    using ShowMeNow.API.Models.RelationModeles;

    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("name=AuthContext")
        {
     
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Itinerary> Itineraries { get; set; }
    }
}