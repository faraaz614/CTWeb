using log4net;

namespace CT.Service
{
    public class BaseService
    {
        public readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public long UserID { get; set; }
        public int RoleID { get; set; }
    }
}
