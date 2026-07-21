// Configurations/StudentConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Praksa.API.Model;
using Praksa.API.Model;

namespace Praksa.API.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Ime).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Prezime).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Email).IsRequired().HasMaxLength(150);
            builder.HasIndex(s => s.Email).IsUnique();
            builder.HasIndex(s => s.IndexBroj).IsUnique();

            builder.HasOne(s => s.Project)
                   .WithMany(p => p.Studenti)
                   .HasForeignKey(s => s.ProjectId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}