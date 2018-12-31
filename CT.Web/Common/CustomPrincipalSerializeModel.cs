using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CT.Web.Common
{
    public class CustomPrincipalSerializeModel
    {
        public int RoleId { get; set; }
        public Int64 UserId { get; set; }
        public string UserName { get; set; }
        public string ProfileName { get; set; }
        public string ProfilePicture { get; set; }
        public string[] Roles { get; set; }
    }
}