using EmployeesListWeb.Data;
using EmployeesListWeb.Models;
using EmployeesListWeb.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq;

namespace EmployeesListWeb.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
         /*   foreach (var t in _context.Employees)
            {
                t.Department.ToString();


            }*/
            var employess = await _context.Employees.ToListAsync();
            
            return View(employess);
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmploeeViewModel addEmploeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmploeeRequest.Name,
                Email = addEmploeeRequest.Email,
                Salary = addEmploeeRequest.Salary,
                DateOfBirth = addEmploeeRequest.DateOfBirth,
                Department = addEmploeeRequest.Department
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee!=null) 
            {
                var editModel = new EditEmployee()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department
                };
                return View(editModel);
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditEmployee model)
        {
            var employee = await _context.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                 employee.Name = model.Name;
                 employee.Email = model.Email;
                 employee.Salary = model.Salary;
                 employee.DateOfBirth = model.DateOfBirth;
                 employee.Department =  model.Department;

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditEmployee model)
        {
            var employee = await _context.Employees.FindAsync(model.Id);

            if(employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
