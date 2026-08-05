using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Infrastructure.DTO
{
    public class ResponseDto
    {

        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public object? Result{ get; set; }
    }
}
