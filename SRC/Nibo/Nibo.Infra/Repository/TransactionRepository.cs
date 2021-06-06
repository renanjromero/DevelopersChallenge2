using Nibo.Domain.Interfaces;
using Nibo.Domain.Models;
using Nibo.Infra.Context;

namespace Nibo.Infra.Repository
{
    public class TransactionRepository: BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(NiboContext niboContext):base(niboContext)
        {
        }
    }
}
