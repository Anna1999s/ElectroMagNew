using DomainModel.Content;
using DomainModel.Localization;
using DomainModel.Navigation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Page> Pages { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        //public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<New> News { get; set; }
        #region Navigation

        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        #endregion

        #region Localization

        public DbSet<Culture> Cultures { get; set; }
        public DbSet<LocalizationSet> LocalizationSets { get; set; }
        public DbSet<DomainModel.Localization.Localization> Localizations { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Culture>(etb =>
            {
                etb.HasKey(e => e.Code);
                etb.Property(e => e.Name).IsRequired().HasMaxLength(64);
                etb.ToTable("Cultures");
            });

            modelBuilder.Entity<DomainModel.Localization.Localization>(etb =>
            {
                etb.HasKey(e => new { e.LocalizationSetId, e.CultureCode });
                etb.ToTable("Localizations");
            });

            modelBuilder.Entity<Photo>()
                    .HasOne(e => e.Product)
                    .WithMany(e => e.Photos)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}