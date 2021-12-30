using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FilmsJust.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;

namespace FilmsJust.Controllers
{
public class GeneroController : Controller
{
    private readonly Context _context;
    public GeneroController(Context context)
    {
        _context = context;
    }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Generos.Include(i => i.Filmes).OrderBy(c => c.GeneroNome).ToListAsync());
        }
        [HttpGet]
        public IActionResult NovoGenero(int generoId)
        {       
            Genero genero = new Genero
            {
                GeneroId= generoId
            };
            return View();
        }     
        [HttpPost]
        public async Task<IActionResult> NovoGenero(Genero genero)
        {            
             await _context.Generos.AddAsync(genero);
             await _context.SaveChangesAsync();
              return RedirectToAction(nameof(Index));
         }
         [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            Genero genero = await _context.Generos.FindAsync(Id);
            return View(genero);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Genero genero)
        {
        _context.Generos.Update(genero);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
        }        
        public async Task<IActionResult> Delete(int? id)
        {            
            var genero = await _context.Generos.FirstOrDefaultAsync(m => m.GeneroId == id);           
            return View(genero);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]         public  async Task<IActionResult> DeleteConfirmed(int? id)
        {
             var genero = await _context.Generos.FindAsync(id);
            _context.Generos.Remove(genero);           
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));           
            
        }        
    }    
}