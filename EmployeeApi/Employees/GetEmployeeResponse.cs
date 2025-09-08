using EmployeeApi;
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

    public class GetEmployeeResponseEmployeeBenefit
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Cost { get; set; }
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
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
    }
}

public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeRequest>
{
    private readonly HttpContext _httpContext;
    private readonly AppDbContext _appDbContext;

    public UpdateEmployeeRequestValidator(IHttpContextAccessor httpContextAccessor, AppDbContext appDbContext)
    {
        this._httpContext = httpContextAccessor.HttpContext!;
        this._appDbContext = appDbContext;

        RuleFor(x => x.Address1).MustAsync(NotBeEmptyIfItIsSetOnEmployeeAlreadyAsync).WithMessage("Address1 must not be empty.");
    }

    private async Task<bool> NotBeEmptyIfItIsSetOnEmployeeAlreadyAsync(string? address, CancellationToken token)
    {
        await Task.CompletedTask;   //again, we'll not make this async for now!

        var id = Convert.ToInt32(_httpContext.Request.RouteValues["id"]);
        var employee = await _appDbContext.Employees.FindAsync(id);

        if (employee!.Address1 != null && string.IsNullOrWhiteSpace(address))
        {
            return false;
        }

        return true;
    }
}
