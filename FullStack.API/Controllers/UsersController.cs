using FullStack.API.Data;
using FullStack.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly FullStackDBContext fullStackDBContext;
        public UsersController(FullStackDBContext fullStackDBContext)
        {
            this.fullStackDBContext = fullStackDBContext;
        }

        // Get Toti Utilizatorii

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await fullStackDBContext.Users.ToListAsync();

            return Ok(users);
        }

        // Post Utilizatri

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User userRequest)
        {

            var existingUser = await fullStackDBContext.Users.FirstOrDefaultAsync(u => u.Email == userRequest.Email);
            if (existingUser != null)
            {
                return Ok(existingUser); // Return a 409 Conflict status if user already exists
            }

            await fullStackDBContext.Users.AddAsync(userRequest);
            await fullStackDBContext.SaveChangesAsync();

            return Ok(userRequest);
        }

        // GET 1 Utilzator

        [HttpGet]

        // ID-ul Utilizatorlui
        [Route("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            var user = await fullStackDBContext.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT Utilizator

        [HttpPut]

        // ID-ul Utilizatoruui
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string id, User updateUserRequest)
        {
            // Asignarea angajatului cu datele din baza de date

            var user = await fullStackDBContext.Users.FindAsync(id);

            // Verificarea daca utilizatorul este null

            if (user == null)
            {
                return NotFound();
            }

            // Modificarea datelor utilizatoruli

            user.Name = updateUserRequest.Name;
            user.Email = updateUserRequest.Email;

            // Actualizarea bazei de date
            await fullStackDBContext.SaveChangesAsync();

            // Return status 
            return Ok(user);
        }

        // DELETE Utilizator

        [HttpDelete]

        // ID-ul Utilizatorui
        [Route("{id}")]

        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            // Asignarea utilizatorului cu datele din baza de date

            var user = await fullStackDBContext.Users.FindAsync(id);

            // Verificarea daca utilizatorul este null

            if (user == null)
            {
                return NotFound();
            }

            // Stergerea Utilizatorului din baza de date
            fullStackDBContext.Users.Remove(user);

            // Actualizarea bazei de date
            await fullStackDBContext.SaveChangesAsync();

            // Return status 
            return Ok(user);
        }

        // GET Id Uilizator

        [HttpGet]

        [Route("login")]
        public async Task<IActionResult> GetUserId(string email)
        {
            var user = await fullStackDBContext.Users.FirstOrDefaultAsync(x => (x.Email == email));

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.UserId); ;
        }
    }
}
