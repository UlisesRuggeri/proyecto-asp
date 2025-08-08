using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using EsteroidesToDo.Infrastructure.Data;

namespace EsteroidesToDo.Infrastructure.DesignTime
{
    public class EsteroidesToDoDbContextFactory : IDesignTimeDbContextFactory<EsteroidesToDoDbContext>
    {
        public EsteroidesToDoDbContext CreateDbContext(string[] args)
        {
            /*
             
            ❌ No es parte del core de acceso a datos.

            ✅ Es una herramienta de diseño (por eso "DesignTime").

            */
            var optionsBuilder = new DbContextOptionsBuilder<EsteroidesToDoDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-NCM9G1T\\SQLEXPRESS;Trusted_Connection=True;Database=EsteroidesToDo;MultipleActiveResultSets=True;TrustServerCertificate=True");

            return new EsteroidesToDoDbContext(optionsBuilder.Options);
        }
    }
}
