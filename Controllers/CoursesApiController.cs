using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourseApp.Data;
using StudentCourseApp.Models; // pentru clasa Course
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentCourseApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/courses
        [HttpGet]
        public async Task<IEnumerable<Course>> GetCourses()
        {
            return await _context.Courses
                .Include(c => c.Teacher) // dacă vrei să incluzi profesorul la serializare
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Teacher)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ID == id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // GET: api/courses/search?title=xyz
        // Exemplu suplimentar de filtrare: 
        [HttpGet("search")]
        public async Task<IEnumerable<Course>> SearchCourses([FromQuery] string title)
        {
            return await _context.Courses
                .Include(c => c.Teacher)
                .AsNoTracking()
                .Where(c => c.Title.Contains(title))
                .ToListAsync();
        }
    }
}
