using Authentication.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Respository
{

    public class IdDbContext : IdentityDbContext<MyUser, Role, int,
                                IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                IdentityRoleClaim<int>, IdentityUserToken<int>>
    {


        public DbSet<Product> Products { get; set; }
        public IdDbContext(DbContextOptions<IdDbContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Company>(cia =>
            {
                cia.ToTable("Companies");
                cia.HasKey(x => x.Id);
                cia.HasMany<MyUser>()
                .WithOne()
                .HasForeignKey(fk => fk.CompanyId)
                .IsRequired(false);

            });

            builder.Entity<UserRole>(userRole =>
            {

                userRole.HasKey(usr => new { usr.UserId, usr.RoleId });

                userRole.HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();

                userRole.HasOne(x => x.MyUser)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId)
                .IsRequired();


            });


        }
    }
}