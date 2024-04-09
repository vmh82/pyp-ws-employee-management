using EmployeeManagement.Entity;

namespace EmployeeManagement.Domain.Services
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(int employeeId);
        Task<int> Create(Employee employee);
        Task<int> Update(Employee employee);
        Task<int> Delete(Employee employee);
        Task<Employee>CheckEmployeeEmail(string email);
    }
}
