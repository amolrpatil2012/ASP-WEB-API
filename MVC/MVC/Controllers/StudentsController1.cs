using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;

namespace MVC.Controllers
{
    public class StudentsController : Controller
    {
        ApplicationDBContext dbContext;

        public StudentsController ( ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel model)
        {
            var student = new Student()
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Subscribed = model.Subscribed
            };
            await dbContext.Students.AddAsync(student); 
            await dbContext.SaveChangesAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();
            return View(students);
        }
    }
}
