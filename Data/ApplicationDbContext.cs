using Microsoft.EntityFrameworkCore;
using PersonalManagementApp.Models;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Veritabanı tablolarını DbSet ile tanımla
    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<Expense> Expenses { get; set; }

    // Model yapılandırmaları (fluent API) burada yapılabilir
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // İlişkiler ve diğer ayarlar burada yapılabilir
    }
}
