﻿namespace CT.Common.Common
{
    public class GenericErrorInfo
    {
        public int ErrorNumber { get; set; }
        public int ErrorSeverity { get; set; }
        public int ErrorState { get; set; }
        public string ErrorProcedure { get; set; }
        public int ErrorLine { get; set; }
        public string ErrorMessage { get; set; }
    }
}
