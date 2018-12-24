namespace CT.Common.Common
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            GenericErrorInfo = new GenericErrorInfo();
            ResponseStatus = new ResponseStatus();
        }
        public GenericErrorInfo GenericErrorInfo { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
        public int RoleID { get; set; }
        public long UserID { get; set; }
    }
}
