using EsteroidesToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace EsteroidesToDo.Data
{
    public class EsteroidesToDoDbContext : DbContext
    {
        public EsteroidesToDoDbContext(DbContextOptions<EsteroidesToDoDbContext> options)
    : base(options) { }

        public DbSet<UsuarioProyectoRoles> UsuarioProyectoRoles { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<ConversacionesTarea> ConversacionesTarea { get; set; }
        public DbSet<EstadosTarea> EstadosTareas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.UsuarioAsignado)
                .WithMany()
                .HasForeignKey(f => f.UsuarioAsignadoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.UsuarioCreador)
                .WithMany()
                .HasForeignKey(f => f.UsuarioCreadorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.UsuarioAsignador)
                .WithMany()
                .HasForeignKey(f => f.UsuarioAsignadorId)
                .OnDelete(DeleteBehavior.NoAction);
            
        }
    }
}

