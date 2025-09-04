using EmployeeApi.Abstractions;
using EmployeeApi.Employees;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    public class EmployeesController : BaseController
    {
        private readonly IRepository<Employee> _repository;
        private readonly ILogger<EmployeesController> _logger;
        public EmployeesController(IRepository<Employee> repository, ILogger<EmployeesController> logger)
        {
            _repository = repository;
            _logger = logger;
           
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetEmployeeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Starting retrieval of all employees");
            var employees = _repository.GetAll().Select(employee=>new GetEmployeeResponse
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Address1 = employee.Address1,
                Address2 = employee.Address2,
                City = employee.City,
                State = employee.State,
                ZipCode = employee.ZipCode,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email
            });
            return Ok(employees);

        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetEmployeeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _repository.GetById(id);
            if(employee == null)
            {
                return NotFound();
            }
            var employeeResponse = new GetEmployeeResponse
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Address1 = employee.Address1,
                Address2 = employee.Address2,
                City = employee.City,
                State = employee.State,
                ZipCode = employee.ZipCode,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email
            };
            return Ok(employeeResponse);
        }
        [HttpPost]
        [ProducesResponseType(typeof(GetEmployeeResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public  ActionResult  CreateEmployee([FromBody] CreateEmployeeRequest employeeRequest)
        {
            var newEmployee = new Employee
            {
                FirstName = employeeRequest.FirstName!,
                LastName = employeeRequest.LastName!,
                SocialSecurityNumber = employeeRequest.SocialSecurityNumber,
                Address1 = employeeRequest.Address1,
                Address2 = employeeRequest.Address2,
                City = employeeRequest.City,
                State = employeeRequest.State,
                ZipCode = employeeRequest.ZipCode,
                PhoneNumber = employeeRequest.PhoneNumber,
                Email = employeeRequest.Email
            };
            _repository.Create(newEmployee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployee.id }, newEmployee);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(GetEmployeeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, [FromBody] UpdateEmployeeRequest employee)
        {
            var existingEmployee = _repository.GetById(id);
            if(existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.Address1 = employee.Address1;
            existingEmployee.Address2 = employee.Address2;
            existingEmployee.City = employee.City;
            existingEmployee.State = employee.State;
            existingEmployee.ZipCode = employee.ZipCode;
            existingEmployee.PhoneNumber = employee.PhoneNumber;
            existingEmployee.Email = employee.Email;

            _repository.Update(existingEmployee);
            return Ok(existingEmployee);

        }





    }
}
