using PersonalManagementApp.Models;

public class Expense
{
    public int ExpenseId { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public bool IsApproved { get; set; }

    // Bağlantılar (Relationship)
    public int EmployeeId { get; set; }  // Foreign Key
    public Employee Employee { get; set; } // Her masraf bir çalışana aittir
}

