using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace CT.Web.Common
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            if (Roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public CustomPrincipal(string userName)
        {
            this.Identity = new GenericIdentity(userName);
        }
        public Int64 UserId { get; set; }
        public Int64 EmployeeID { get; set; }
        public Int64 RoleId { get; set; }
        public string UserName { get; set; }
        public string ProfileName { get; set; }
        public string ProfileName_EN { get; set; }
        public string ProfilePicture { get; set; }
        public string[] Roles { get; set; }
    }
}