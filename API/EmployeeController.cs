using Microsoft.AspNetCore.Mvc;
using PersonalManagementApp.Models;
//BURDA BÝR HATA OLABÝLÝR EMÝN DEÐÝLÝM
[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEmployees()
    {
        var employees = await _unitOfWork.Employees.GetAllAsync();
        return Ok(employees);
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployee(Employee employee)
    {
        await _unitOfWork.Employees.AddAsync(employee);
        await _unitOfWork.SaveChangesAsync();
        return Ok(employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
    {
        var existingEmployee = await _unitOfWork.Employees.GetByIdAsync(id);
        if (existingEmployee == null)
            return NotFound();

        existingEmployee.FirstName = employee.FirstName;
        existingEmployee.LastName = employee.LastName;

        _unitOfWork.Employees.Update(existingEmployee);
        await _unitOfWork.SaveChangesAsync();

        return Ok(existingEmployee);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id);
        if (employee == null)
            return NotFound();

        _unitOfWork.Employees.Delete(employee);
        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }
}
