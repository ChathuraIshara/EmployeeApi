namespace EmployeeApi
{
    public class Employee
    {
        public int id { get; set; }
        public required string FirstName { get; set;}  //since we add required,this does not give nullable warning.otherwise we need to add ? in the type to avoid it
        public required string LastName { get; set; }

    }
}
