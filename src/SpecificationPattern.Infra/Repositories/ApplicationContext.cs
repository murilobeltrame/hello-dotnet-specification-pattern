using Microsoft.EntityFrameworkCore;
using SpecificationPattern.Domain.Entities;

namespace SpecificationPattern.Infra.Repositories
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Wine> Wines { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}
