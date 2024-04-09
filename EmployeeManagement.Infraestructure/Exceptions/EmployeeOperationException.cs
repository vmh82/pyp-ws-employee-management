using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infraestructure.Exceptions
{
    public class EmployeeOperationException : Exception
    {
        public EmployeeOperationException()
        {
        }

        public EmployeeOperationException(string message) : base(message)
        {
        }

        public EmployeeOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
