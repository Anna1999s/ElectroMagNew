using DomainModel.Account;
using DomainModel.Catalog;
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

        public DbSet<Message> Messages { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Page> Pages { get; set; }

        #region Content

        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<VehicleMark> VehicleMarks { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<VehicleOption> VehicleOptions { get; set; }
        public DbSet<VehicleColor> VehicleColors { get; set; }
        public DbSet<DriveType> DriveTypes { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<Country> ManufacturerCountries { get; set; }
        public DbSet<TransmissionType> TransmissionTypes { get; set; }
        public DbSet<BodyType> BodyTypes { get; set; }


        public DbSet<Organization> Organizations  { get; set; }
        public DbSet<Filial> Filials  { get; set; }
        public DbSet<FilialOption> FilialOptions  { get; set; }
        public DbSet<Service> Services  { get; set; }
        public DbSet<ServiceCategory> ServiceCategories  { get; set; }
        public DbSet<ServiceType> ServiceTypes  { get; set; }
        public DbSet<ServicePrice> ServicePrices  { get; set; }
        public DbSet<PaymentType> PaymentTypes  { get; set; }
        public DbSet<Emploeer> Emploeers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Box> Boxes { get; set; }

        #endregion

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

            //modelBuilder.Entity<IdentityUser>().ToTable("AspNetUsers", "dto");
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
        }
    }
}