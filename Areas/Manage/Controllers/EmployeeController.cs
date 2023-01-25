using eBusiness.DAL;
using eBusiness.Models;
using eBusiness.Utilies.Extension;
using eBusiness.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eBusiness.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class EmployeeController : Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;

        public EmployeeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Employees.Include(p => p.Position));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var employee = await _context.Employees.FirstOrDefaultAsync(p => p.Id == id);
            if (employee == null) return NotFound();
            employee.ImageUrl.DeleteFile(_env.WebRootPath, "assets/img");
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVM employeeVM)
        {
            if (!_context.Positions.Any(p => p.Id == employeeVM.PositionId)) ModelState.AddModelError("PositionId", "bele bir position yoxdu");
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
                return View();
            }
            string result = employeeVM.Image.CheckValidate(500,"image/");
            if (result.Length > 0)
            {
                ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
                ModelState.AddModelError("Image", result);
                return View();
            }
            Employee employee = new Employee
            {
                Name = employeeVM.Name,
                Surname = employeeVM.Surname,
                PositionId = employeeVM.PositionId,
                FacebookUrl = employeeVM.FacebookUrl,
                InstagramUrl = employeeVM.InstagramUrl,
                TwitterUrl = employeeVM.TwitterUrl,
                Salary = employeeVM.Salary,
                ImageUrl = employeeVM.Image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img"))
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
            var employee =await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null) { return NotFound(); }
            UpdateEmployeeVM employeeVM = new UpdateEmployeeVM
            {
                Name = employee.Name,
                Surname = employee.Surname,
                PositionId = employee.PositionId,
                FacebookUrl = employee.FacebookUrl,
                TwitterUrl = employee.TwitterUrl,
                InstagramUrl = employee.InstagramUrl,
                Salary = employee.Salary,
                ImgUrl=employee.ImageUrl
            };
            return View(employeeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,UpdateEmployeeVM employeeVM)
        {
            ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
            var existedemployee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (existedemployee == null) { return NotFound(); }
            if (!_context.Positions.Any(p => p.Id == employeeVM.PositionId)) ModelState.AddModelError("PositionId", "bele bir position yoxdu");
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
                return View();
            }
            if (employeeVM.Image != null)
            {
                string result = employeeVM.Image.CheckValidate(500, "image/");
                if (result.Length > 0)
                {
                    ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
                    ModelState.AddModelError("Image", result);
                    return View();
                }
                existedemployee.ImageUrl.DeleteFile(_env.WebRootPath, "assets/img");
                existedemployee.ImageUrl = employeeVM.Image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img"));
            }
            existedemployee.Name= employeeVM.Name;
            existedemployee.Surname= employeeVM.Surname;
            existedemployee.Salary= employeeVM.Salary;
            existedemployee.PositionId=employeeVM.PositionId;
            existedemployee.FacebookUrl= employeeVM.FacebookUrl;
            existedemployee.InstagramUrl= employeeVM.InstagramUrl;
            existedemployee.TwitterUrl= employeeVM.TwitterUrl;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
