using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Configuration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            // Table name
            builder.ToTable("Bookings");

            // Composite Primary Key
            builder.HasKey(b => new { b.FestivalId, b.UserId });

            // Properties
            builder.Property(b => b.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.Price)
                .IsRequired();

            // Relationships
            builder.HasOne(b => b.Festival)
                .WithMany() // optionally: .WithMany(f => f.Bookings) if you add a collection on Festival
                .HasForeignKey(b => b.FestivalId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
