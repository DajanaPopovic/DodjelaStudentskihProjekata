// Configurations/MentorConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Praksa.API.Model;

namespace Praksa.API.Configurations
{
    public class MentorConfiguration : IEntityTypeConfiguration<Mentor>
    {
        public void Configure(EntityTypeBuilder<Mentor> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Email).IsRequired().HasMaxLength(150);
            builder.HasIndex(m => m.Email).IsUnique();
        }
    }
}