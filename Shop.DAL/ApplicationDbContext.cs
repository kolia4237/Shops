using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entity;

namespace Shop.DAL
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Cloth> Product { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Basket> Basket { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cloth>(builder =>
            {
                builder.ToTable("Cloth").HasKey(x => x.Id);

                builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd();
                builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(70);
                builder.Property(p => p.Description).HasColumnName("Description").HasMaxLength(1000);
                builder.Property(x => x.Price).HasColumnName("Price");
                builder.Property(x => x.Gender).HasColumnName("Gender");

                /*builder.HasOne(u => u.Basket)
                    .WithMany(p => p.Cloths)
                    .HasForeignKey(p => p.BasketId);*/
            });
            
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("User").HasKey(x => x.Id);
                
                builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd();
                builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(70);
                builder.Property(p => p.Password).HasColumnName("Password").HasMaxLength(50);
                builder.Property(x => x.Role).HasColumnName("Role").HasMaxLength(40);
            });

            modelBuilder.Entity<Basket>(builder =>
            {
                builder.ToTable("Basket").HasKey(x => x.Id);
                
                builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd();
                builder.Property(x => x.ClothId).HasColumnName("ClothId");
                builder.Property(p => p.UserId).HasColumnName("UserId");


                /*builder.HasOne(u => u.User)
                    .WithOne(p => p.Basket)
                    .HasForeignKey<Basket>(p => p.ClothId);*/
            });
        }
    }
}