using Microsoft.EntityFrameworkCore;
using recipeservice.Model;

namespace recipeservice.Data {
    public class ApplicationDbContext : DbContext {

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Phase> Phases { get; set; }
        public DbSet<PhaseParameter> PhaseParameters { get; set; }
        public DbSet<PhaseProduct> PhaseProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ExtraAttibruteType> ExtraAttibruteTypes { get; set; }
        public DbSet<AdditionalInformation> AdditionalInformations { get; set; }

        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) {

        }
        protected override void OnModelCreating (ModelBuilder modelBuilder) {

        }
    }
}