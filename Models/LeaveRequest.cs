using System;


namespace PersonalManagementApp.Models
{
    public class LeaveRequest
    {
        public int LeaveRequestId { get; set; }
        public string Reason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaveType { get; set; }
       
        public bool IsApproved { get; set; }

        // Bağlantılar (Relationship)
        public int EmployeeId { get; set; }  // Foreign Key
        public Employee Employee { get; set; } // Her izin talebi bir çalışanla ilişkilidir
    }
}