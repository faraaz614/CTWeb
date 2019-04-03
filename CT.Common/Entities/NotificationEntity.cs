using CT.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Common.Entities
{
    public class NotificationEntity : BaseEntity
    {
        public long ID { get; set; }
        public long VehicleID { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public long NotificationTo { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
