namespace ASP.NET_ONETOMANY.Models;

public class Department
{
    public int DepartmentId { get; set; }
    public string Name { get; set; }

    // Initialize the collection to avoid validation issues
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}