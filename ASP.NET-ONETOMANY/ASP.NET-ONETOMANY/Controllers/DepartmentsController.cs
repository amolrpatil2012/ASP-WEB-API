using ASP.NET_ONETOMANY.Data;
using ASP.NET_ONETOMANY.Exceptions;
using ASP.NET_ONETOMANY.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_ONETOMANY.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : Controller
{
    private readonly AppDbContext _context;

    public DepartmentsController(AppDbContext context)
    {
        _context = context;
    }
    // Get all departments with their employees
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
    {
        return await _context.Departments
            .Include(d => d.Employees)
            .ToListAsync();
    }

    // Get a specific department with employees
    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> GetDepartment(int id)
    {
        var department = await _context.Departments
            .Include(d => d.Employees)
            .FirstOrDefaultAsync(d => d.DepartmentId == id);

        if (department == null)
        {
            throw new NotFoundException("The requested resource was not found.");
            //return NotFound();
        }

        return department;
    }

    // Create a new department
    [HttpPost]
    public async Task<ActionResult<Department>> CreateDepartment(Department department)
    {
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDepartment), new { id = department.DepartmentId }, department);
    }

    // Add an employee to a department
    [HttpPost("{id}/employees")]
    public async Task<ActionResult<Employee>> AddEmployee(int id, Employee employee)
    {
        var department = await _context.Departments.FindAsync(id);

        if (department == null)
        {
            return NotFound();
        }

        employee.DepartmentId = id;
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDepartment), new { id = id }, employee);
    }
}
