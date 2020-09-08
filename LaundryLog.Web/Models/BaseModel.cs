using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryLog.Web.Models
{
    public class BaseModel
    {
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
