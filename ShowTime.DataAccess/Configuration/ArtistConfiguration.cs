using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Configuration
{
    public class ArtistConfiguration : IEntityTypeConfiguration<Artists>
    {
        public void Configure(EntityTypeBuilder<Artists> builder)
        {
            builder.ToTable("Artists");
            // cheie primara
            builder.HasKey(a => a.Id);
            // configurare proprietati
            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(a => a.Genre)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(a => a.Image)
                .IsRequired()
                .HasMaxLength(500);
            // configurare relatii
            builder.HasMany(a => a.Festivals)
                .WithMany(f => f.Artists)
                .UsingEntity<Lineup>(); // Tabel de legatura pentru relatia many-to-many
        }
    }
}
