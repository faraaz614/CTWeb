using CT.Common.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Common.Entities
{
    public class BaseVehicleEntity : BaseEntity
    {
        public BaseVehicleEntity()
        {
            VehicleEntity = new VehicleEntity();
            ListVehicles = new List<VehicleEntity>();
        }

        public VehicleEntity VehicleEntity { get; set; }
        public List<VehicleEntity> ListVehicles { get; set; }
    }

    public class VehicleEntity : BaseEntity
    {
        public VehicleEntity()
        {
            DocumentDetail = new DocumentDetailEntity();
            TechnicalDetail = new TechnicalDetailEntity();
            VehicleDetail = new VehicleDetailEntity();
            VehicleImage = new VehicleImageEntity();
        }

        public long ID { get; set; }

        [Required]
        [StringLength(150)]
        [MinLength(6, ErrorMessage = "Minimum length should be 6 characters")]
        public string VehicleName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDealClosed { get; set; }
        public bool IsDelete { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public DocumentDetailEntity DocumentDetail { get; set; }
        public TechnicalDetailEntity TechnicalDetail { get; set; }
        public VehicleDetailEntity VehicleDetail { get; set; }
        public VehicleImageEntity VehicleImage { get; set; }
    }
}
