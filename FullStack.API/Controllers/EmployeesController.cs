using FullStack.API.Data;
using FullStack.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        /* Get Angajati */

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
    }
}
