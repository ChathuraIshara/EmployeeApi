using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class BaseController:Controller
    {

    }
}
