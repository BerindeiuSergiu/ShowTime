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
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");
            // cheie primara
            builder.HasKey(a => a.Id);
            // configurare proprietati
            // properties
            builder.Property(b => b.TicketType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.Price)
                .IsRequired();
            builder.Property(b => b.Quantity)
                .IsRequired();
            // configurare relatii

            builder.HasOne(a => a.Festival)
                .WithMany(b => b.Tickets)
                .HasForeignKey(a => a.FestivalId)
                .OnDelete(DeleteBehavior.NoAction);




        }
    }
}
