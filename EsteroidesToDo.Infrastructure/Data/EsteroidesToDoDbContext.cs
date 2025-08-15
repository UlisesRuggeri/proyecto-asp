using EsteroidesToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace EsteroidesToDo.Infrastructure.Data
{
    public class EsteroidesToDoDbContext : DbContext
    {
        public EsteroidesToDoDbContext(DbContextOptions<EsteroidesToDoDbContext> options)
            : base(options) { }

        // -------------------
        // DbSets
        // -------------------
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<ConversacionesTarea> ConversacionesTarea { get; set; }
        public DbSet<EstadosTarea> EstadosTareas { get; set; }
        public DbSet<Postulante> UsuarioVacantes { get; set; }
        public DbSet<Vacante> Vacantes { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<UsuarioProyectoRol> UsuarioProyectoRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Postulante>().ToTable("Postulantes");



            // -------------------
            // Primary Keys
            // -------------------
            modelBuilder.Entity<UsuarioProyectoRol>()
                .HasKey(up => new { up.UsuarioId, up.ProyectoId });

            modelBuilder.Entity<Postulante>()
                .HasKey(uv => new { uv.UsuarioId, uv.VacanteId });

            modelBuilder.Entity<Notificacion>()
                .HasKey(n => new { n.IdEmpresa, n.IdUsuario });

            modelBuilder.Entity<ConversacionesTarea>()
                .HasKey(ct => new { ct.TareaId, ct.UsuarioId });

            // -------------------
            // Relaciones
            // -------------------

            // Postulante -> Vacante
            modelBuilder.Entity<Postulante>()
                .HasOne(uv => uv.Vacante)
                .WithMany(v => v.UsuarioVacantes)
                .HasForeignKey(uv => uv.VacanteId)
                .OnDelete(DeleteBehavior.NoAction);

            // Postulante -> Usuario
            modelBuilder.Entity<Postulante>()
                .HasOne(uv => uv.Usuario)
                .WithMany(u => u.UsuarioVacantes)
                .HasForeignKey(uv => uv.UsuarioId)
                .OnDelete(DeleteBehavior.NoAction);

            // Tarea -> UsuarioAsignado
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.UsuarioAsignado)
                .WithMany()
                .HasForeignKey(t => t.UsuarioAsignadoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Tarea -> UsuarioCreador
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.UsuarioCreador)
                .WithMany()
                .HasForeignKey(t => t.UsuarioCreadorId)
                .OnDelete(DeleteBehavior.NoAction);

            // Tarea -> UsuarioAsignador
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.UsuarioAsignador)
                .WithMany()
                .HasForeignKey(t => t.UsuarioAsignadorId)
                .OnDelete(DeleteBehavior.NoAction);

            // Empresa -> Duenio
            modelBuilder.Entity<Empresa>()
                .HasOne(e => e.Duenio)
                .WithMany()
                .HasForeignKey(e => e.IdDuenio)
                .OnDelete(DeleteBehavior.NoAction);

            // Usuario -> Empresa
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Empresa)
                .WithMany(e => e.Usuarios)
                .HasForeignKey(u => u.EmpresaId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
