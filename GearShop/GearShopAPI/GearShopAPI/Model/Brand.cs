using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GearShopAPI.Model
{
    public class Brand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        ICollection<CPU> CPUs { get; set; }
        ICollection<HeadPhone> headPhones { get; set; }
        ICollection<KeyBoard> keyBoards { get; set; }
    }
}
