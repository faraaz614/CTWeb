using CT.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Common.Entities
{
    public class TechnicalDetailEntity : BaseEntity
    {
        public long ID { get; set; }
        public string Engine { get; set; }
        public string Body { get; set; }
        public string SuspensionSteeringSystem { get; set; }
        public string Transmission { get; set; }
        public string Electrical { get; set; }
        public string AirCondition { get; set; }
        public string Brakes { get; set; }
        public string TyresCondition { get; set; }
        public string OtherInformation { get; set; }
        public long VehicleID { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
