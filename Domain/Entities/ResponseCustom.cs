using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ResponseCustom
    {
        public dynamic result { get; set; }
        public dynamic message { get; set; }
        public bool status { get; set; }
    }
}
