using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Configuration;
using ShowTime.DataAccess.Models;

/*
 *  in modulul acesta putem realiza query-uri si legaturi dintre bd si cod
 * 
 * 
 * 
 * 
 * 
 */


namespace ShowTime.DataAccess
{
    public class ShowTimeContext: DbContext
    {
        public ShowTimeContext(DbContextOptions<ShowTimeContext> options) : base(options)
        {
        }


        public DbSet<Festival> Festivals { get; set; }
        public DbSet<Artists> Artists { get; set; }
        public DbSet<Lineup> Lineups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Booking> Ticket { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShowTimeContext).Assembly);
            base.OnModelCreating(modelBuilder);

            new ArtistConfiguration().Configure(modelBuilder.Entity<Artists>());
            new FestivalConfiguration().Configure(modelBuilder.Entity<Festival>());
            new LineupConfiguration().Configure(modelBuilder.Entity<Lineup>());
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
            new BookingConfiguration().Configure(modelBuilder.Entity<Booking>());
            new TicketConfiguration().Configure(modelBuilder.Entity<Ticket>());
        }
    }
}
