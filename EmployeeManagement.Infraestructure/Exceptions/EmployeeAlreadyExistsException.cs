using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infraestructure.Exceptions
{
    public class EmployeeAlreadyExistsException : Exception
    {
        public EmployeeAlreadyExistsException()
        {
        }

        public EmployeeAlreadyExistsException(string message) : base(message)
        {
        }

        public EmployeeAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
