namespace P01_StudentSystem.Data.Configurations
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder
                .Property(b => b.Name)
                .IsUnicode(true);

            builder
                .Property(b => b.Url)
                .IsUnicode(false);

            builder
                .HasOne(r => r.Course)
                .WithMany(c => c.Resources)
                .HasForeignKey(r => r.ResourceId);
        }
    }
}
