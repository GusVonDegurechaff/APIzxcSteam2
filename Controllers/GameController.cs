using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly DBForISGameContext _context;

        public GameController(DBForISGameContext context)
        {
            _context = context;
        }
        // GET: api/game
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetAll()
        {
            return await _context.Games.ToListAsync();
        }

        // GET: api/game/{id}
        [HttpGet("{Id}")]
        public async Task<ActionResult<Game>> GetById(int Id)
        {
            var games = await _context.Games.FindAsync(Id);
            if (games is null)
                return NotFound();

            return games;
        }

        // POST: api/games
        [HttpPost]
        public async Task<IActionResult> Create(Game game)
        {
            try
            {
                _context.Games.Add(game);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { Id = game.GameId }, game);
            }
            catch (DbUpdateException)
            {
                // Обработка исключения
                return BadRequest("Ошибка при создании Игры.");
            }
        }

        // PUT: api/games/{id}
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutGame(int Id, Game game)
        {
            if (Id != game.GameId)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool GameExists(int Id)
        {
            return _context.Games.Any(e => e.GameId == Id);
        }

        // DELETE: api/game/{id}
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteGame(int Id)
        {
            var game = await _context.Games.FindAsync(Id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        //    [HttpGet("byCourse/{kyrs}")]
        //    public async Task<ActionResult<List<Student>>> GetStudentsByCourse(int kyrs)
        //    {
        //        var students = await _context.Students.Where(s => s.Kyrs == kyrs).ToListAsync();
        //        return students.Any() ? Ok(students) : NotFound();
        //    }
        //    [HttpGet("{id}/{kyrs}")]
        //    public async Task<ActionResult<Student>> GetStudentByIdAndCourse(int id, int kyrs)
        //    {
        //        var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id && s.Kyrs == kyrs);
        //        return student is null ? NotFound() : Ok(student);
        //    }
        //    // DELETE: api/students/{id and kyrs}
        //    [HttpDelete("{id}/{kyrs}")]
        //    public async Task<IActionResult> DeleteStudent2(int id, int kyrs)
        //    {
        //        var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id && s.Kyrs == kyrs);
        //        if (student == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.Students.Remove(student);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }
        //}
        //}
    }
}
