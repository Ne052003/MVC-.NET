using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Infrastructure.DTO.Supplier
{
    public class UpdateSupplierDto : AddSupplierDto
    {
        public int SupplierId { get; set; }
    }
}
