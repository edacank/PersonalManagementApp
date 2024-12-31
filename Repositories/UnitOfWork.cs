using PersonalManagementApp.Models;

namespace PersonalManagementApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IRepository<Employee> Employees { get; private set; }
        public IRepository<Company> Companies { get; private set; }
        public IRepository<Expense> Expenses { get; private set; }
        public IRepository<LeaveRequest> LeaveRequests { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Employees = new Repository<Employee>(context);
            Companies = new Repository<Company>(context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
