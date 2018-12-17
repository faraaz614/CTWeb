using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Common.Common
{
    public class CTApiResponse
    {
        public CTApiResponse()
        {

        }

        public CTApiResponse(bool issuccess, string message)
        {
            IsSuccess = issuccess;
            Message = message;
        }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
