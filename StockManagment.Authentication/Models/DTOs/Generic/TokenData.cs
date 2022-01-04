using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.Authentication.Models.DTOs.Generic
{
    public class TokenData
    {
        [Required]
        public string JwtToken { get; set; }

        [Required]
        public string RefreshToken { get; set; }

    }
}
