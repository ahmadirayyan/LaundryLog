using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryLog.Web.Models
{
    public class LauLog
    {
        public int Id { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }

        public ICollection<LauUnit> LauUnits { get; set; }
    }
}
