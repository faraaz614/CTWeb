using CT.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Common.Entities
{
    public class DashEntity : BaseEntity
    {
        public DashEntity()
        {
            dashEntities = new List<DashEntity>();
        }

        public string c1 { get; set; }
        public string c2 { get; set; }
        public string c3 { get; set; }
        public string c4 { get; set; }
        public string c5 { get; set; }
        public string rowtype { get; set; }

        public List<DashEntity> dashEntities { get; set; }
    }
}
