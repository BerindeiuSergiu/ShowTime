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

            // relationships  

            // A festival can have multiple bookings  
            builder.HasOne(b => b.Festival)
                .WithMany(f => f.Bookings)
                .HasForeignKey(b => b.FestivalId)
                .OnDelete(DeleteBehavior.Cascade);

            // A user can have multiple bookings  
            builder.HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

 
            builder.HasOne(b => b.Ticket)
                .WithOne(t => t.Booking)
                .HasForeignKey<Booking>(b => b.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
