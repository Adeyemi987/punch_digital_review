using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarService.Core.Models.APIResponse
{
    public class GenericAPIResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }

    public class GenericAPIResponse<T> : GenericAPIResponse
    {
        public T Data { get; set; }
    }
}
