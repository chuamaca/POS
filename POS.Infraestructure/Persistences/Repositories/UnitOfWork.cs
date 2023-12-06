using POS.Domain.Entities;
using POS.Infraestructure.Persistences.Context;
using POS.Infraestructure.Persistences.Interfaces;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly PosContext _context;
        public ICategoryRepository Category {  get; private set; }

        public UnitOfWork(PosContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task saveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
