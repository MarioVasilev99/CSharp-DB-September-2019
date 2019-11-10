namespace P03_FootballBetting.Data.Configurations
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity
                .Property(e => e.Username)
                .IsUnicode(false);

            entity
                .HasIndex(e => e.Username)
                .IsUnique();

            entity
                .Property(e => e.Password)
                .IsUnicode(false);

            entity
               .Property(e => e.Email)
               .IsUnicode(false);

            entity
                .HasIndex(e => e.Email)
                .IsUnique();
        }
    }
}
