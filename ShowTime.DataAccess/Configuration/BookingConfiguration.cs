using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Configuration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");

            // primary key
            builder.HasKey(b => new { b.FestivalId, b.UserId });

            // properties
            builder.Property(b => b.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.Price)
                .IsRequired();

            // relationships

            // un festival poate avea mai multe booking-uri
            builder.HasOne(b => b.Festival)
                .WithMany(f => f.Bookings)
                .HasForeignKey(b => b.FestivalId)
                .OnDelete(DeleteBehavior.Cascade);

            // un user poate avea mai multe booking-uri
            builder.HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
