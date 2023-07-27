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

            // Asignarea de Utilizator

            var users = await fullStackDBContext.Users.ToListAsync();

            // Returnarea de Utilizator

            return Ok(users);
        }

        // Post Utilizatri

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User userRequest)
        {

            // Asignarea Utilizatorului care are emailul selectat

            var existingUser = await fullStackDBContext.Users.Where(u => u.Email == userRequest.Email).Include(x => x.Role).FirstOrDefaultAsync();

            // Vreificarea daca Uilizatorul este in baza de date

            if (existingUser != null)
            {
                return Ok(existingUser); // Return a 409 Conflict status if user already exists
            }

            //

            var existingDepartment = fullStackDBContext.Roles.FirstOrDefault(d => d.RoleId == userRequest.RoleId);
            if (existingDepartment == null)
            {
                // If the department with the given ID does not exist, handle it accordingly.
                // For example, you can throw an exception or log an error message.
                throw new ArgumentException($"Department with ID {userRequest.RoleId} does not exist.");
            }

            userRequest.Role = existingDepartment;

            // Adaugarea in baza de date

            await fullStackDBContext.Users.AddAsync(userRequest);

            // Salvarea datelor schimbate

            await fullStackDBContext.SaveChangesAsync();

            // Returnarea Utilizatorulu

            return Ok(userRequest);
        }

        // GET 1 Utilzator

        [HttpGet]

        // ID-ul Utilizatorlui
        [Route("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {

            // Asignaea Utilizatorului care are Id-ul cerut

            var user = await fullStackDBContext.Users.FirstOrDefaultAsync(x => x.UserId == id);

            // Vrificarea daca utilizatorul este null

            if (user == null)
            {
                return NotFound();
            }

            // Returnarea Utilizatorului

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

            user.UserId = updateUserRequest.UserId;
            user.Email = updateUserRequest.Email;
            user.Name = updateUserRequest.Name;
            user.Username = updateUserRequest.Username;
            user.Nickname = updateUserRequest.Nickname;
            user.Picture = updateUserRequest.Picture;
            user.Role = updateUserRequest.Role;

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
    }
}
