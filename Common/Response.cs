using System;
using System.Net;

namespace Common
{
    public class Response<TEntity>
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public TEntity Result { get; set; }
        public string Message { get; set; }
        public int Code { get; set; } = 0;
    }
}
