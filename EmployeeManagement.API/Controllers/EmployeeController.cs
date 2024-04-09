using EmployeeManagement.Domain.Services;
using EmployeeManagement.Entity;
using EmployeeManagement.Entity.Dto.Request;
using EmployeeManagement.Entity.Dto.Response;
using EmployeeManagement.Infraestructure.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EmployeeManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeInfraestructure _employeeInfraestructure;

        public EmployeeController(IEmployeeInfraestructure employeeInfraestructure)
        {
            _employeeInfraestructure = employeeInfraestructure;
         }

        [HttpGet]
        [Route("employees")]
        [Authorize(Roles = "admin, employee")]
        public async Task<ActionResult> GetEmployees()
        {
            ResponseBase<List<EmployeeResponseDto>> response = await _employeeInfraestructure.GetEmployees();
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "admin, employee")]
        public async Task<ActionResult> GetEmployeeById([FromQuery]int employeeId)
        {
            ResponseBase<Employee> response =  await _employeeInfraestructure.GetEmployeeById(employeeId);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Post(EmployeeRequestDto employeeRequestDto)
        {
            EmployeeRequestDtoValidator validator = new EmployeeRequestDtoValidator();
            ValidationResult validationResult = validator.Validate(employeeRequestDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(q => q.ErrorMessage));
            }
            ResponseBase<Employee> response = await _employeeInfraestructure.Create(employeeRequestDto);
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Put(EmployeeRequestDto employeeRequestDto)
        {
            EmployeeRequestDtoValidator validator = new EmployeeRequestDtoValidator();
            ValidationResult validationResult = validator.Validate(employeeRequestDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(q => q.ErrorMessage));
            }
            ResponseBase<int> response = await _employeeInfraestructure.Update(employeeRequestDto);
            return Ok(response);
        }

        [HttpDelete("{employeeId}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int employeeId)
        {
            ResponseBase<int> response = await _employeeInfraestructure.Delete(employeeId);
            return Ok(response);
        }
    }
}
