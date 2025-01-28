using System.Text.Json.Serialization;

namespace ASP.NET_ONETOMANY.Models;

public class Employee
{
    public int EmployeeId { get; set; }
    public string FullName { get; set; }

    // Foreign key
    public int DepartmentId { get; set; }

    // Navigation property to the related department
    [JsonIgnore]
    public Department? Department { get; set; }
}