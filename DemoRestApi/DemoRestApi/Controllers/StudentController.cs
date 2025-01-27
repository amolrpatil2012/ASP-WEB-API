using DemoRestApi.Models;
using DemoRestApi.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace DemoRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        //[HttpGet]
        //public IEnumerable<StudentDTO> GetStudents()
        //{
        //    return StudentData.studentList;
        //}
        //[HttpGet("{id:int}")]
        //public StudentDTO GetStudent(int id)
        //{
        //    return StudentData.studentList.FirstOrDefault(s => s.Id == id);
        //}

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            return Ok(StudentData.studentList);
        }

        [HttpGet("{id:int}", Name = "GetStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> GetStudent(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var std = StudentData.studentList.FirstOrDefault(s => s.Id == id);
            if (std == null)
            {
                return NotFound();
            }
            return Ok(std);
        }

        [HttpPost]
        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (student == null)
            {
                return BadRequest(student);
            }
            if (student.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            student.Id = StudentData.studentList.OrderByDescending(student => student.Id).First().Id + 1;
            // return Ok(student);
            return CreatedAtRoute("GetStudent", new { id = student.Id }, student);
        }


        [HttpDelete("{id:int}", Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> DeleteStudent(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var std = StudentData.studentList.FirstOrDefault(s => s.Id == id);
            if (std == null)
            {
                return NotFound();
            }
            StudentData.studentList.Remove(std);
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDTO> UpdateStudent(int id, [FromBody] StudentDTO student)
        {
            if (id == 0 || id != student.Id)
            {
                return BadRequest();
            }
            StudentDTO dTO = StudentData.studentList.FirstOrDefault(student => student.Id == id);
            dTO.Name = student.Name;
            return NoContent();


        }
    }
}
