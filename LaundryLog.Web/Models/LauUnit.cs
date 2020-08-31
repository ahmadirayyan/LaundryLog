using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryLog.Web.Models
{
    public class LauUnit
    {
        public int Id { get; set; }
        public int LauItemId { get; set; }
        public int LauLogId { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }

        public LauItem LauItem { get; set; }
        public LauLog LauLog { get; set; }
    }
}
