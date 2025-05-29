using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products.UpdateStock
{
    public record UpdateProductStockRequest(int id,int quantity);
}
