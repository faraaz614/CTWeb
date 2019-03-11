using CT.Common.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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
            BidDurationList = new List<SelectListItem>
            {
                new SelectListItem { Text = "30 minutes", Value = "30", Selected = true },
                new SelectListItem { Text = "60 minutes", Value = "60" }
            };
            DocumentDetail = new DocumentDetailEntity();
            TechnicalDetail = new TechnicalDetailEntity();
            VehicleDetail = new VehicleDetailEntity();
            VehicleImage = new List<VehicleImageEntity>();
            VehicleBIDs = new List<VehicleBIDEntity>();
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
        public long? NotificationVID { get; set; }
        public int BidDurationID { get; set; }
        public List<SelectListItem> BidDurationList { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public string Variant { get; set; }
        public string YearOfManufacturing { get; set; }
        public int Kilometers { get; set; }
        public string Transmission { get; set; }
        public string RegistrationNo { get; set; }
        public string Type { get; set; }
        public bool IsRCavailable { get; set; }
        public bool Hypothication { get; set; }
        public bool IsNOCavailable { get; set; }
        public int NoOfOwners { get; set; }
        public int NoOfKeys { get; set; }
        public bool IsInsuranceAvailable { get; set; }
        public bool IsComprehensive { get; set; }
        public bool IsThirdParty { get; set; }
        public string InsuranceExpiryDate { get; set; }
        public decimal BIDAmount { get; set; }
        public DateTime? BidTime { get; set; }
        public long? BidTimeMilliSecs { get; set; }

        public DocumentDetailEntity DocumentDetail { get; set; }
        public TechnicalDetailEntity TechnicalDetail { get; set; }
        public VehicleDetailEntity VehicleDetail { get; set; }
        public List<VehicleImageEntity> VehicleImage { get; set; }
        public List<VehicleBIDEntity> VehicleBIDs { get; set; }
        public List<Combo> FuelTypeList { get; set; }
    }
}
