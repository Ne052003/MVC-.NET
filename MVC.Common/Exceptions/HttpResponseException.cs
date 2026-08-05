using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Common.Exceptions
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; }
        public Object Value { get; set; } = null!;
    }
}
