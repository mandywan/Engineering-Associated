using System.Collections.Generic;
using AeDirectory.DTO;
using AeDirectory.Domain;

namespace AeDirectory.Services
{
    public interface IEmployeeService
    {
        List<EmployeeDTO> GetEmployeeList();
        EmployeeDTO GetEmployeeByEmployeeNumber(int id);
        List<EmployeeDTO> GetEmployeeByFilters(Filter filters);

    }
}