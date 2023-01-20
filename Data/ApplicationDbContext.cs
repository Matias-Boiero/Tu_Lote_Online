using Microsoft.EntityFrameworkCore;
using TuLote.Models;

namespace TuLote.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Localidad> Localidades { get; set; }
        public DbSet<Barrio> Barrios { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Tipo> Tipos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Provincia>()
        .Property(p => p.Id)
        .ValueGeneratedNever();

            modelBuilder.Entity<Municipio>()
        .Property(p => p.Id)
        .ValueGeneratedNever();

            modelBuilder.Entity<Localidad>()
        .Property(p => p.Id)
        .ValueGeneratedNever();

        }
    }
}
