using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GearShopAPI.Model
{
    public class CPU
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public decimal Warranty { get; set; }
        public string Hinh { get; set; }
        public string Name { get; set; }
        public decimal Status { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brands { get; set; }
    }
}
