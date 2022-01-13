using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.Entities.DbSet
{
    public class User:BaseEntity
    {
        public Guid Identity { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime DateOfBirth { get; set; }



        public IList<UserWarehouses> UserWarehouses { get; set; }

    }
}
