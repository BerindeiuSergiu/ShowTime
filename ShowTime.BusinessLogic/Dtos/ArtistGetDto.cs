﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BussinessLogic.Dtos
{
    public class ArtistGetDto
    {

        public ArtistGetDto()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
