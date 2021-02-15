using System.Collections.Generic;
using AeDirectory.DTO;

namespace AeDirectory.Services
{
    public interface IEmployeeService
    {
        List<EmployeeDTO> GetEmployeeList();
        EmployeeDTO GetEmployeeByEmployeeNumber(int id);
    }
}