namespace CT.Service.Entities
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
    }
}
