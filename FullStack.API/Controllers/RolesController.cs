using FullStack.API.Data;
using FullStack.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RestSharp;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly FullStackDBContext fullStackDBContext;
        public RolesController(FullStackDBContext fullStackDBContext)
        {
            this.fullStackDBContext = fullStackDBContext;
        }

        /* Get Toate Departamentele */

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await fullStackDBContext.Roles.ToListAsync();

            return Ok(roles);
        }

        /* Post Departament */

        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] Role roleRequest)
        {
            if (roleRequest == null)
            {
                return BadRequest("Role request null");
            }

            //Id unic

            roleRequest.RoleId = Guid.NewGuid();

            await fullStackDBContext.Roles.AddAsync(roleRequest);
            await fullStackDBContext.SaveChangesAsync();

            return Ok(roleRequest);
        }

        /* GET 1 Departament */

        [HttpGet]

        // ID-ul Departamentului
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRole([FromRoute] Guid id)
        {
            var role = await fullStackDBContext.Roles.FirstOrDefaultAsync(x => x.RoleId == id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        /* PUT Departament */

        [HttpPut]

        // ID-ul Departamentului
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRole([FromRoute] Guid id, Role updateRoleRequest)
        {
            // Asignarea departamentului cu datele din baza de date

            var role = await fullStackDBContext.Roles.FindAsync(id);

            // Verificarea daca departamentul este null

            if (role == null)
            {
                return NotFound();
            }

            // Modificarea datelor departamentului

            role.RoleId = updateRoleRequest.RoleId;
            role.Name = updateRoleRequest.Name;

            // Actualizarea bazei de date

            await fullStackDBContext.SaveChangesAsync();

            // Return status 
            return Ok(role);
        }

        /* DELETE Departament */

        [HttpDelete]

        // ID-ul Department
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteRole([FromRoute] Guid id)
        {
            // Asignarea Department cu datele din baza de date

            var role = await fullStackDBContext.Roles.FindAsync(id);

            // Verificarea daca angajatul este null

            if (role == null)
            {
                return NotFound();
            }

            // Stergerea Departamentului din baza de date
            fullStackDBContext.Roles.Remove(role);

            // Actualizarea bazei de date
            await fullStackDBContext.SaveChangesAsync();

            // Return status 
            return Ok(role);
        }

        /* Creare Rol In Auth0  

        [HttpPost]

        // Ruta

        [Route("auth0")]

        public async Task<IActionResult> AddRoleAuth0([FromBody] Role roleRequest)
        {
            var client = new RestClient("https://{yourDomain}/api/v2/roles");
            RestRequest request = new RestRequest(Method.Post);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Bearer MGMT_API_ACCESS_TOKEN");
            request.AddHeader("cache-control", "no-cache");
            request.AddParameter("application/json", "{ \"name\": \"ROLE_NAME\", \"description\": \"ROLE_DESC\" }", ParameterType.RequestBody);
            RestResponse response = client.Execute(request);

            return Ok(roleRequest);
        }
        */
    }
}
