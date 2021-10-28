using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MaintenanceCode
    {
        public int IdCode { get; set; }
        public int idCliente { get; set; }
        public string Code { get; set; }
        public DateTime TimeExpire { get; set; }
        public string Status { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
