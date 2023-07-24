using FullStack.API.Data;
using FullStack.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : Controller
    {
        private readonly FullStackDBContext fullStackDBContext;
        public DepartmentsController(FullStackDBContext fullStackDBContext)
        {
            this.fullStackDBContext = fullStackDBContext;
        }

        /* Get Toate Departamentele */

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await fullStackDBContext.Departments.ToListAsync();

            return Ok(departments);
        }

        /* Post Departament */

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] Department departmentRequest)
        {
            if (departmentRequest == null)
            {
                return BadRequest("Employee request null");
            }
            //Id unic
            departmentRequest.DepartmentId = Guid.NewGuid();

            await fullStackDBContext.Departments.AddAsync(departmentRequest);
            await fullStackDBContext.SaveChangesAsync();

            return Ok(departmentRequest);
        }

        /* GET 1 Departament */

        [HttpGet]

        // ID-ul Departamentului
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetDepartment([FromRoute] Guid id)
        {
            var department = await fullStackDBContext.Departments.FirstOrDefaultAsync(x => x.DepartmentId == id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        /* PUT Departament */

        [HttpPut]

        // ID-ul Departamentului
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] Guid id, Department updateDepartmentRequest)
        {
            // Asignarea departamentului cu datele din baza de date

            var department = await fullStackDBContext.Departments.FindAsync(id);

            // Verificarea daca departamentul este null

            if (department == null)
            {
                return NotFound();
            }

            // Modificarea datelor departamentului

            department.DepartmentId = updateDepartmentRequest.DepartmentId;
            department.Name = updateDepartmentRequest.Name;

            // Actualizarea bazei de date

            await fullStackDBContext.SaveChangesAsync();

            // Return status 
            return Ok(department);
        }

        /* DELETE Departament */

        [HttpDelete]

        // ID-ul Department
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteDepartment([FromRoute] Guid id)
        {
            // Asignarea Department cu datele din baza de date

            var department = await fullStackDBContext.Departments.FindAsync(id);

            // Verificarea daca angajatul este null

            if (department == null)
            {
                return NotFound();
            }

            // Stergerea Departamentului din baza de date
            fullStackDBContext.Departments.Remove(department);

            // Actualizarea bazei de date
            await fullStackDBContext.SaveChangesAsync();

            // Return status 
            return Ok(department);
        }
    }
}
