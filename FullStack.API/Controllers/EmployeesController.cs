using FullStack.API.Data;
using FullStack.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly FullStackDBContext fullStackDBContext;
        public EmployeesController(FullStackDBContext fullStackDBContext)
        {

            // Asigneaza baza de date la variabila

            this.fullStackDBContext = fullStackDBContext;
        }

        /* Get Toti Angajatii */

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllEmployees()
        {

            // Asigneaza toti angajatii la variabla incluzand departamentele aferente

            var employees = await fullStackDBContext.Employees.Include(x => x.Department).ToListAsync();

            // Returneaza angajatul

            return Ok(employees);
        }

        /* Post Angajati */

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {

            // Verifica daca angajatul este null

            if(employeeRequest == null)
            {
                return BadRequest("Employee request null");
            }

            // Adauga Id unic

            employeeRequest.EmployeeId = Guid.NewGuid();

            // Verifica daca Id-ul e departament este deja in baza de date

            var existingDepartment = fullStackDBContext.Departments.FirstOrDefault(d => d.DepartmentId == employeeRequest.DepartmentId);
            if (existingDepartment == null)
            {
                // If the department with the given ID does not exist, handle it accordingly.
                // For example, you can throw an exception or log an error message.
                throw new ArgumentException($"Department with ID {employeeRequest.DepartmentId} does not exist.");
            }

            employeeRequest.Department = existingDepartment;


            // Adauga la baza de date noul angajat

            await fullStackDBContext.Employees.AddAsync(employeeRequest);

            // Salveaza modificarile

            await fullStackDBContext.SaveChangesAsync();

            // Returneaz angajatul

            return Ok(employeeRequest);
        }

        /* GET 1 Angajat */

        [HttpGet]

        // ID-ul Angajatului
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {

            // Asigenaza angajatul cu Id-ul specificat

            var employee = await fullStackDBContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);

            // Returneaza ca nu l-a gasit daca este null

            if (employee == null)
            {
                return NotFound();
            }

            // Returneaza Angajatul

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
            employee.DepartmentId = updateEmployeeRequest.DepartmentId;
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
