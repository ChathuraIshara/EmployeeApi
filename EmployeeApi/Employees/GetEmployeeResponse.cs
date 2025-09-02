using EmployeeApi.Employees;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace EmployeeApi.Employees
{
    public class CreateEmployeeRequest
    {
        [Required(AllowEmptyStrings = false)]
        public  string? FirstName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public  string? LastName { get; set; }
        public string? SocialSecurityNumber { get; set; }

        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }

    public class GetEmployeeResponse
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }

    public class UpdateEmployeeRequest
    {
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}

public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
{
    public CreateEmployeeRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First  name is required");
        RuleFor(x => x.LastName).NotEmpty();
    }
}
