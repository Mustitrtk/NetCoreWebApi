using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    public record CreateProductRequest(string Name, decimal Price, int Stock);
}
