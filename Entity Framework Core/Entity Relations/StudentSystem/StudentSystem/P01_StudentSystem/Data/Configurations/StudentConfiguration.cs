namespace P01_StudentSystem.Data.Configurations
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> entity)
        {
            entity
                .Property(e => e.Name)
                .IsUnicode(true);

            entity
                .Property(e => e.PhoneNumber)
                .IsUnicode(false)
                .IsFixedLength(true);
        }
    }
}
