using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiyorMarket.Domain.Exceptions
{
    public class LowStockException : Exception
    {
        public LowStockException() { }
        public LowStockException(string message) : base(message) { }
    }
}
