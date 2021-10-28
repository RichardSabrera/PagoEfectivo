using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Parameters
    {
        public string parameter { get; set; }
        public SqlDbType type { get; set; }
        public dynamic value { get; set; }
    }
}
