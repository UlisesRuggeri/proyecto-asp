using EsteroidesToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace EsteroidesToDo.Infrastructure.Data
{
    public class EsteroidesToDoDbContext : DbContext
    {
        public EsteroidesToDoDbContext(DbContextOptions<EsteroidesToDoDbContext> options)
    : base(options) { }

        public DbSet<UsuarioVacante> UsuarioVacantes { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<Vacante> Vacantes { get; set; }
        public DbSet<UsuarioProyectoRol> UsuarioProyectoRoles { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<ConversacionesTarea> ConversacionesTarea { get; set; }
        public DbSet<EstadosTarea> EstadosTareas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UsuarioVacante>()
                .HasKey(uv => new { uv.UsuarioId, uv.VacanteId });

            modelBuilder.Entity<UsuarioVacante>()
                .HasOne(uv => uv.Vacante)
                .WithMany(v => v.UsuarioVacantes)  
                .HasForeignKey(uv => uv.VacanteId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UsuarioVacante>()
                .HasOne(uv => uv.Usuario)
                .WithMany(u => u.UsuarioVacantes)  
                .HasForeignKey(uv => uv.UsuarioId)
                .OnDelete(DeleteBehavior.NoAction);


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

            modelBuilder.Entity<Empresa>()
                .HasOne(e => e.Duenio)
                .WithMany()  
                .HasForeignKey(e => e.IdDuenio)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Empresa)
                .WithMany(e => e.Usuarios)  
                .HasForeignKey(u => u.EmpresaId)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}

