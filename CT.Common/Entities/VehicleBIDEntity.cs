using CT.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Common.Entities
{
    public class VehicleBIDEntity : BaseEntity
    {
        public long ID { get; set; }
        public long VehicleID { get; set; }
        public decimal? BIDAmount { get; set; }
        public string Description { get; set; }
        public long DealerID { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
