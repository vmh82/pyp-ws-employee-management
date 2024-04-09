using EmployeeManagement.Domain.Services;
using EmployeeManagement.Entity;
using EmployeeManagement.Entity.Dto.Request;
using EmployeeManagement.Entity.Dto.Response;
using EmployeeManagement.Infraestructure.Exceptions;
using Mapster;
using MapsterMapper;
using System;

namespace EmployeeManagement.Infraestructure.Services
{
    public class EmployeeInfraestructure : IEmployeeInfraestructure
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly  IMapper _mapper;
       
        public EmployeeInfraestructure(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<bool> CheckEmployeeEmail(string email)
        {
            Employee employeeEmail = await _employeeRepository.CheckEmployeeEmail(email);
            if (employeeEmail is not null)
            {
                throw new EmployeeAlreadyExistsException("The email is already registered");
            }
            return true;
        }

        public async Task<ResponseBase<Employee>> Create(EmployeeRequestDto employeeRequestDto)
        {
            await CheckEmployeeEmail(employeeRequestDto.Email);
            Employee employeeMapper =  await _mapper.From(employeeRequestDto).AdaptToTypeAsync<Employee>();
            int employeeId = await _employeeRepository.Create(employeeMapper);
            if(employeeId == 0)
            {
                throw new EmployeeOperationException("An error ocurred when delete the employee");
            }
            Employee employee = await _employeeRepository.GetEmployeeById(employeeId);
            return new ResponseBase<Employee> { Data = employee, Message = "Employee save successfully" };
        }

        public async Task<ResponseBase<int>> Delete(int employeeId)
        {
            Employee employee = await _employeeRepository.GetEmployeeById(employeeId);
            if(null == employee)
            {
                throw new EmployeeNotFoundException("Employee not found");
            }
            int result = await _employeeRepository.Delete(employee);
            string message = result == 1 ? "Employee delete successfully" : "An error ocurred when delete the employee";
            return new ResponseBase<int> { Data = result,  Message = message };

        }

        public async Task<ResponseBase<Employee>> GetEmployeeById(int employeeId)
        {
            Employee employee = await _employeeRepository.GetEmployeeById(employeeId);
            return employee is null
                ? throw new EmployeeNotFoundException("Employee not found")
                : new ResponseBase<Employee> { Data = employee, Message = $" results found"  };
        }

        public async Task<ResponseBase<List<EmployeeResponseDto>>> GetEmployees()
        {
            List<Employee> employees = await _employeeRepository.GetEmployees();
            if (!employees.Any())
            {
                throw new EmployeeNotFoundException("Not results found");
            }
            List<EmployeeResponseDto> employeeResponse = await _mapper.From(employees).AdaptToTypeAsync<List<EmployeeResponseDto>>();
            return new ResponseBase<List<EmployeeResponseDto>> { Data = employeeResponse, Message = $"{employeeResponse.Count} results found"};
        }

        public async Task<ResponseBase<int>> Update(EmployeeRequestDto employeeRequestDto)
        {
            Employee employee = await _employeeRepository.GetEmployeeById(employeeRequestDto.Id);

            if (null == employee)
            {
                throw new EmployeeNotFoundException("Employee not found");
            }
            if (!employee.Email.Equals(employeeRequestDto.Email))
            {
                await CheckEmployeeEmail(employeeRequestDto.Email);
            }
            Employee employeeMapper = await _mapper.From(employeeRequestDto).AdaptToTypeAsync<Employee>();
            int result = await _employeeRepository.Update(employeeMapper);
            string message = result == 1 ? "Employee update successfully" : "An error ocurred when update the employee";
            return new ResponseBase<int> { Data = result, Message = message };
        }
    }
}
