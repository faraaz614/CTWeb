using CT.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Common.Entities
{
    public class BaseVehicleBIDEntity : BaseEntity
    {
        public BaseVehicleBIDEntity()
        {
            ListBids = new List<VehicleBIDEntity>();
            VehicleBIDEntity = new VehicleBIDEntity();
            VehicleEntity = new VehicleEntity();
        }

        public List<VehicleBIDEntity> ListBids { get; set; }
        public VehicleBIDEntity VehicleBIDEntity { get; set; }
        public VehicleEntity VehicleEntity { get; set; }
    }
    public class VehicleBIDEntity : BaseEntity
    {
        public long ID { get; set; }
        public long VehicleID { get; set; }
        public string VehicleName { get; set; }
        public string StockID { get; set; }
        public decimal? BIDAmount { get; set; }
        public string Description { get; set; }
        public long DealerID { get; set; }
        public string DealerName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
