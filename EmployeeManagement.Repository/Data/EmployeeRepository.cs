using EmployeeManagement.Domain.Services;
using EmployeeManagement.Entity;
using EmployeeManagement.Repository.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EmployeeManagement.Repository.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<Employee> CheckEmployeeEmail(string email)
        {
          return await _employeeDbContext.Employee.AsNoTracking().Where(q=>q.Email.Equals(email)).FirstOrDefaultAsync();
        }

        public async Task<int> Create(Employee employee)
        {
            _employeeDbContext.Employee.Add(employee);
            await _employeeDbContext.SaveChangesAsync();
            return employee.Id;
        }

        public async Task<int> Delete(Employee employee)
        {
            _employeeDbContext.Employee.Remove(employee);
            return await _employeeDbContext.SaveChangesAsync();
        }

        public async Task<Employee> GetEmployeeById(int idEmployee)
        {
            return await _employeeDbContext.Employee.AsNoTracking().Where(q => q.Id.Equals(idEmployee)).FirstOrDefaultAsync();
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _employeeDbContext.Employee.AsNoTracking().ToListAsync();
        }

        public async Task<int> Update(Employee employee)
        {
            SqlParameter[] parameters = new SqlParameter[]
             {
                new SqlParameter("@i_employee_id", employee.Id),
                new SqlParameter("@i_first_name", employee.FirstName),
                new SqlParameter("@i_last_name", employee.LastName),
                new SqlParameter("@i_email", employee.Email),
                new SqlParameter("@i_position", employee.Position),
                new SqlParameter("@i_phone", employee.Phone),
                new SqlParameter("@i_address", employee.Address),

             };
            return await _employeeDbContext.Database.ExecuteSqlRawAsync("EXEC spu_update_employee @i_employee_id, @i_first_name, @i_last_name, @i_email, @i_position, @i_phone, @i_address", parameters);
        }
    }
}
