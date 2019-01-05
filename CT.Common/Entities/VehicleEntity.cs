using CT.Common.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
            VehicleImage = new List<VehicleImageEntity>();
            FuelTypeList = new List<Combo>();
        }

        public long ID { get; set; }
        [Required(ErrorMessage ="Car Name is required.")]
        [StringLength(150)]
        [MinLength(6, ErrorMessage = "Car Name should be 6 characters")]
        public string VehicleName { get; set; }
        [Required(ErrorMessage = "StockID is required.")]
        [StringLength(150)]
        [MinLength(6, ErrorMessage = "StockID should be 6 characters")]
        public string StockID { get; set; }
        public string ImageName { get; set; }
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
        public List<VehicleImageEntity> VehicleImage { get; set; }
        public List<Combo> FuelTypeList { get; set; }
    }
}
