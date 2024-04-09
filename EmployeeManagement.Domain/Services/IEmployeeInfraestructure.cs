using EmployeeManagement.Entity;
using EmployeeManagement.Entity.Dto.Request;
using EmployeeManagement.Entity.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Services
{
    public interface IEmployeeInfraestructure
    {
        Task<ResponseBase<List<EmployeeResponseDto>>> GetEmployees();
        Task<ResponseBase<Employee>>GetEmployeeById(int employeeId);
        Task<ResponseBase<Employee>> Create(EmployeeRequestDto employeeRequestDto);
        Task<ResponseBase<int>> Update(EmployeeRequestDto employeeRequestDto);
        Task<ResponseBase<int>> Delete(int employeeId);
        Task<bool> CheckEmployeeEmail(string email);

    }
}
