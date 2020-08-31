using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryLog.Web.Models
{
    public class LauItem
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime DateBought { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public ICollection<LauUnit> LauUnits { get; set; }
    }
}
