// Configurations/ProjectConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Praksa.API.Model;

namespace Praksa.API.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Naziv).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Status).HasConversion<string>();

            builder.HasOne(p => p.Mentor)
                   .WithMany(m => m.Projekti)
                   .HasForeignKey(p => p.MentorId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}