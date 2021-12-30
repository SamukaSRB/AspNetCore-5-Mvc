using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FilmsJust.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;


namespace FilmsJust.Controllers
{
    public class LocacaoController : Controller
    {
        private readonly Context _context;        
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LocacaoController(Context context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        } 
        public async Task<IActionResult> Index()
         {           
            return View(await _context.Filmes.Include(i => i.Genero).OrderBy(c => c.Nome).ToListAsync());  
         }
           public async Task<IActionResult> Venda(int? id)
        {        
            var imagens = _context.Filmes.Select(m => m.FilmeId).ToList();
            var filme =await _context.Filmes.Include(i => i.Genero).OrderBy(c => c.Nome).FirstOrDefaultAsync(m => m.FilmeId == id);         
// 
          return View(filme);
        }     
    }
         
  
    

}

