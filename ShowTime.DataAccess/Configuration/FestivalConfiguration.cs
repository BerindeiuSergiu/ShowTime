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
    public class FestivalConfiguration : IEntityTypeConfiguration<Festival>
    {

        public void Configure(EntityTypeBuilder<Festival> builder)
        {
            builder.ToTable("Festivals");
            //  cheie prim
            builder.HasKey(f => f.Id);

            // config prop
            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(f => f.StartDate)
                .IsRequired();
            builder.Property(f => f.EndDate)
                .IsRequired();
            builder.Property(f => f.Location)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(f => f.SplashArt)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(f => f.Capacity)
                .IsRequired()
                .HasDefaultValue(0); // Optional: set default value for capacity


            // config relatii
            // cu line-up-uri mai multe festivale pot apartine unui singur lineup
            builder.HasMany(f => f.Lineups)
                .WithOne(l => l.Festival)
                .HasForeignKey(l => l.FestivalID);
            // un festival poate apartine la mai multe booking-uri
            builder.HasMany(f => f.Bookings)
                .WithOne(b => b.Festival)
                .HasForeignKey(b => b.FestivalId);
        }


    }
}
