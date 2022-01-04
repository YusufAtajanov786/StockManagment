using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.Entities.DbSet
{
    public class RefreshToken : BaseEntity
    {
        public string UserId { get; set; }

        public string Token { get; set; }

        public string JwtId { get; set; }

        public bool isUsed { get; set; } // To make sure the token used or not

        public bool isRevoked { get; set; } //  make sure they are valid

        public DateTime ExpiryDate { get; set; }


        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
    }
}
