using PersonalManagementApp.Models;

public interface IUnitOfWork : IDisposable
{
    IRepository<Employee> Employees { get; }
    IRepository<Company> Companies { get; }
    IRepository<LeaveRequest> LeaveRequests { get; }
    IRepository<Expense> Expenses { get; }
    Task<int> SaveChangesAsync();
}
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IRepository<Employee> Employees { get; private set; }
    public IRepository<Company> Companies { get; private set; }
    public IRepository<LeaveRequest> LeaveRequests { get; private set; }
    public IRepository<Expense> Expenses { get; private set; }
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Employees = new Repository<Employee>(context);
        Companies = new Repository<Company>(context);
        LeaveRequests = new Repository<LeaveRequest>(context);
        Expenses = new Repository<Expense>(context);
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
