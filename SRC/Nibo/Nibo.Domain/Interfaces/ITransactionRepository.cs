using Nibo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nibo.Domain.Interfaces
{
    public interface ITransactionRepository: IEntityBaseRepository<Transaction>
    {
    }
}
