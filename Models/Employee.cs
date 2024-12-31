namespace PersonalManagementApp.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Bağlantılar (Relationship)
        public int CompanyId { get; set; }  // Foreign Key
        public Company Company { get; set; } // Her çalışan bir şirkete ait
    }
}