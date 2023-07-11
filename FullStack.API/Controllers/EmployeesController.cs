using FullStack.API.Data;
using FullStack.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly FullStackDBContext fullStackDBContext;
        public EmployeesController(FullStackDBContext fullStackDBContext)
        {
            this.fullStackDBContext = fullStackDBContext;
        }

        /* Get Toti Angajatii */

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await fullStackDBContext.Employees.ToListAsync();

            return Ok(employees);
        }

        /* Post Angajati */

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            //Id unic
            employeeRequest.Id = Guid.NewGuid();

            await fullStackDBContext.Employees.AddAsync(employeeRequest);
            await fullStackDBContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }

        /* GET 1 Angajat */

        [HttpGet]

        // ID-ul Angajatului
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await fullStackDBContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        /* PUT Angajat */

        [HttpPut]

        // ID-ul Angajatlui
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            // Asignarea angajatului cu datele din baza de date

            var employee = await fullStackDBContext.Employees.FindAsync(id);

            // Verificarea daca angajatul este null

            if (employee == null)
            {
                return NotFound();
            }

            // Modificarea datelor angajatului

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Department = updateEmployeeRequest.Department;

            // Actualizarea bazei de date
            await fullStackDBContext.SaveChangesAsync();

            // Return status 
            return Ok(employee);
        }

        /* DELETE Angajat */

        [HttpDelete]

        // ID-ul Angajatlui
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            // Asignarea angajatului cu datele din baza de date

            var employee = await fullStackDBContext.Employees.FindAsync(id);

            // Verificarea daca angajatul este null

            if (employee == null)
            {
                return NotFound();
            }

            // Stergerea Angajatului din baza de date
            fullStackDBContext.Employees.Remove(employee);

            // Actualizarea bazei de date
            await fullStackDBContext.SaveChangesAsync();

            // Return status 
            return Ok(employee);
        }
    }
}
