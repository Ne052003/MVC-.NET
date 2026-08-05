using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Infrastructure.DTO.Product
{
    public class UpdateProductDto : AddProductDto
    {
        public int ProductId { get; set; }
    }
}
