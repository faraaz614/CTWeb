using CT.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Common.Entities
{
    public class DocumentDetailEntity : BaseEntity
    {
        public long ID { get; set; }
        public bool IsRCavailable { get; set; }
        public bool Hypothication { get; set; }
        public bool IsNOCavailable { get; set; }
        public int NoOfOwners { get; set; }
        public int NoOfKeys { get; set; }
        public bool IsInsuranceAvailable { get; set; }
        public bool IsComprehensive { get; set; }
        public bool IsThirdParty { get; set; }
        public string InsuranceExpiryDate { get; set; }
        public long VehicleID { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
