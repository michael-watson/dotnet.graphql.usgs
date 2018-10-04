using dotnet.graphql.usgs.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet.graphql.usgs
{
    public class SiteDbContext : DbContext
    {
        public SiteDbContext() : base()
        { }

        public SiteDbContext(DbContextOptions<SiteDbContext> options) : base(options)
        { }

        public DbSet<SiteMeta> Sites { get; set; }
        public DbSet<FlowInfo> SiteFlows { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:watzbites.database.windows.net,1433;Initial Catalog=watzbites-db;Persist Security Info=False;User ID=watson;Password=yHuJsoH@FHCG;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}