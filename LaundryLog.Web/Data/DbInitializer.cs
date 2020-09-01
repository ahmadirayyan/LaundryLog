using LaundryLog.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaundryLog.Web.Data
{
    public static class DbInitializer
    {
        public static void Initialize(LauContext context)
        {
            context.Database.EnsureCreated();

            if (context.LauItems.Any())
            {
                return;
            }

            var lauItems = new LauItem[]
            {
                new LauItem{Category="Kaos",Color="Hijau",Brand="Cole",Price=69000,Description="",Status=true,DateBought=DateTime.Now,DateCreated=DateTime.Now,DateModified=DateTime.Now}
            };
            foreach (LauItem l in lauItems)
            {
                context.LauItems.Add(l);
            }
            context.SaveChanges();

            var lauLogs = new LauLog[]
            {
                new LauLog{DateIn=DateTime.Now,DateOut=DateTime.Now,Price=15000,Status=true}
            };
            foreach (LauLog l in lauLogs)
            {
                context.LauLogs.Add(l);
            }
            context.SaveChanges();

            var lauUnits = new LauUnit[]
            {
                new LauUnit{LauItemId=1,LauLogId=1,Quantity=1,Status=1,Notes=""}
            };
            foreach (LauUnit l in lauUnits)
            {
                context.LauUnits.Add(l);
            }
            context.SaveChanges();
        }
    }
}
