using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Contracts.Infrastructure
{
    public interface IUnitOfWork
    {
        public IOrderRepository Orders { get; }
    }
}
