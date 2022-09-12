using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ResponseError
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }
        public int Code { get; set; } = -1;
    }
}
