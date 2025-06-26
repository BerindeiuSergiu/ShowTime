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
    public class LineupConfiguration: IEntityTypeConfiguration<Lineup>
    {
        public void Configure(EntityTypeBuilder<Lineup> builder)
        {
            builder.ToTable("Lineups");
            
        }

    }
}
