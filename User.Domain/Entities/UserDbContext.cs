using Framework.Auth;
using Framework.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace User.Domain.Entities
{
    public class UserDbContext: DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) 
        { 
            
        }
        public DbSet<UserEntity> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Seed();
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasData
                (
                    new UserEntity()
                    {
                        Id = Guid.NewGuid(),
                        UserName = "admin",
                        Password = Encryption.HashSha256("Admin1234!"),
                        FirstName = "Super",
                        LastName = "User",
                        Email = "atur.aritonang@xsis.co.id",
                        Status = RecordStatusEnum.Active,
                        Type = UserTypeEnum.Administrator
                    }
                );
        }
    }
}
