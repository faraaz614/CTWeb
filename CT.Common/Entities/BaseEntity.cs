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
        public string SearchText { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageTotal { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
    }
}
