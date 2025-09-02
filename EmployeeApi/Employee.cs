using System.ComponentModel.DataAnnotations;

namespace EmployeeApi
{
    public class Employee
    {
        public int id { get; set; }
        public required  string FirstName { get; set;}  //since we add required,this does not give nullable warning.otherwise we need to add ? in the type to avoid it
        public required string LastName { get; set; }
        public  string? SocialSecurityNumber { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

    }
}
