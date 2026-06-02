using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Data;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentRepository _repository;

        public StudentsController(IConfiguration configuration)
        {
            _repository = new StudentRepository(configuration);
        }

        // GET all students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            Console.WriteLine(" GET API CALLED");

            var students = _repository.GetAllStudents();
            return Ok(students);
        }

        // get Students by rollno

        [HttpGet("by-rollno/{rollNo}")]
        public IActionResult GetByRollNo(string rollNo)
        {
            var student = _repository.GetStudentByRollNo(rollNo);

            if (student == null)
                return NotFound("No student found");

            return Ok(student);
        }

        // post 

        [HttpPost]
        public IActionResult AddStudent([FromForm] Student student)
        {
            _repository.AddStudent(student);
            return Ok("Student added successfully");
        }

        // delete


        [HttpDelete("{rollNo}")]
        public IActionResult DeleteByRollNo(string rollNo)
        {
            var existing = _repository.GetStudentByRollNo(rollNo);

            if (existing == null)
                return NotFound("Student not found");

            bool deleted = _repository.DeleteStudentByRollNo(rollNo);

            if (!deleted)
                return BadRequest("Delete failed");

            return Ok("Student deleted successfully");
        }





        // update
        [HttpPut("{rollNo}")]
public IActionResult UpdateByRollNo(string rollNo, Student student)
{
    var existing = _repository.GetStudentByRollNo(rollNo);

    if (existing == null)
        return NotFound("No student found with this RollNo");

    bool updated = _repository.UpdateByRollNo(rollNo, student);

    if (!updated)
        return BadRequest("Update failed");

    return Ok("Student updated successfully");
}
    }
}