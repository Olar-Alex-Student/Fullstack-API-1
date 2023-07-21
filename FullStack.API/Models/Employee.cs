using System;

namespace FullStack.API.Models
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long Salary { get; set; }
        public Guid? DepartmentId { get; set; }

        public Department? Department { get; set; }
    }
}
