using Microsoft.EntityFrameworkCore;

namespace Traceability_system.Data
{
    public class YourDbContext : DbContext
    {
        public YourDbContext(DbContextOptions<YourDbContext> options)
            : base(options)
        {
        }

        // 在这里定义你的 DbSet 属性
        // 例如：public DbSet<YourEntity> YourEntities { get; set; }
    }
}