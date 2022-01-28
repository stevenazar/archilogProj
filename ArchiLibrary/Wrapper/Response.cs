using System;
using System.Collections.Generic;
using System.Text;

namespace ArchiLibrary.Wrapper
{
    /// <summary>
    /// CLASS alows us to return other parameters like error messages, response status, page number, data, page size
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Response<T>
    {
        public Response()
        {

        }

        public Response(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
    
    }
}
