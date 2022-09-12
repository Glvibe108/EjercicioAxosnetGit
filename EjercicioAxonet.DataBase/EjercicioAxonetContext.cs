using EjercicioAxonet.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace EjercicioAxonet.DataBase
{
    public class EjercicioAxonetContext : IdentityDbContext
    {
        public EjercicioAxonetContext(DbContextOptions<EjercicioAxonetContext> options)
               : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Monedas> Monedas { get; set; }
        public DbSet<Recibos> Recibos { get; set; }
    }
}
