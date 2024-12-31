using PersonalManagementApp.Models;

public class Company
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string Address { get; set; }
    public ICollection<Employee> Employees { get; set; } // Bir şirkete birden fazla çalışan
}
