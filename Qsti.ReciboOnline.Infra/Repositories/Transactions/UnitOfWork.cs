using Qsti.ReciboOnline.Infra.Repositories.Base;

namespace Qsti.ReciboOnline.Infra.Repositories.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;

        protected UnitOfWork()
        {

        }
        public UnitOfWork(Context context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
