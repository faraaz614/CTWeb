using CT.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Common.Entities
{
    public class VehicleDetailEntity : BaseEntity
    {
        public VehicleDetailEntity()
        {
            FuelTypeList = new List<Combo>();
        }

        public long ID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Variant { get; set; }
        public DateTime YearOfManufacturing { get; set; }
        public int FuelTypeID { get; set; }
        public long VehicleID { get; set; }
        public int? Kilometers { get; set; }
        public string Transmission { get; set; }
        public string RegistrationNo { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public List<Combo> FuelTypeList { get; set; }
    }
}
