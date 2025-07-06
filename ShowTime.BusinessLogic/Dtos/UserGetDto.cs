using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Dtos
{
    public class UserGetDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public int Role { get; set; } // foreign key
    }
}
