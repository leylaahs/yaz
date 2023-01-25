using eBusiness.DAL;
using eBusiness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eBusiness.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class PositionController : Controller
    {
        readonly AppDbContext _context;
        public PositionController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Positions);
        }
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var epositon = await _context.Positions.FirstOrDefaultAsync(p => p.Id == id);
            if (epositon == null) return NotFound();
            _context.Positions.Remove(epositon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Create(Position position)
        {
            if(position == null) { return BadRequest();}
            if (!ModelState.IsValid) return View();
            Position newposition= new Position
            {
                Name = position.Name,
            };
            await _context.Positions.AddAsync(newposition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if(id == null)  return BadRequest(); 
            var positon = await _context.Positions.FirstOrDefaultAsync(p=>p.Id == id);   
            return View(positon);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,Position position)
        {
            if(id == null) return BadRequest();
            var epositon = await _context.Positions.FirstOrDefaultAsync(p => p.Id == id);
            if(epositon == null) return NotFound();
            if (!ModelState.IsValid) return View();
            epositon.Name= position.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
