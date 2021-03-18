using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GearShopAPI.Model
{
    public class UserInformation
    {
        [Key]
        public Guid UserId { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string Sdt { get; set; }
        public string Pass { get; set; }
    }
}
