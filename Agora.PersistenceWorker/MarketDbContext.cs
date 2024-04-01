using Agora.Simulator;
using Microsoft.EntityFrameworkCore;

namespace Agora.PersistenceWorker;

public class MarketDbContext : DbContext
{
    public MarketDbContext(DbContextOptions<MarketDbContext> options) : base(options)
    {
        
    }

    public DbSet<Order> Orders { get; set; }
}