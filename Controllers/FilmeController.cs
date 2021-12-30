using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FilmsJust.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace FilmsJust.Controllers
{
    public class FilmeController : Controller
    {
        private readonly Context _context; 
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FilmeController(Context context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }   
        public async Task<IActionResult> Index()
         {           
            return View(await _context.Filmes.Include(i => i.Genero).OrderBy(c => c.Nome).ToListAsync());  
         }   
         // GET: Produtos        
        public IActionResult Create()
        {  
             var generos = _context.Generos.OrderBy(i => i.GeneroNome).ToList();          
             ViewBag.Generos = generos;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Filme filme, IFormFile imagem)
        {
            if(ModelState.IsValid)
            {
             if(imagem !=null)
             {
                 string diretorio = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                 string nomeImagem = Guid.NewGuid().ToString() + imagem.FileName;
                 using(FileStream fileStream = new FileStream(Path.Combine(diretorio, nomeImagem), FileMode.Create))
                 {
                    await imagem.CopyToAsync(fileStream);
                    filme.Imagem = "~/img/" + nomeImagem;                 

                 }      
                 await _context.Filmes.AddAsync(filme);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             } 

            }          
            return View(filme);
        }         
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {         
            var filme = await _context.Filmes.FindAsync(Id);
            TempData["ImagemFilme"] = filme.Imagem;
            var generos = _context.Generos.OrderBy(i => i.GeneroNome).ToList();           
            ViewBag.Generos =generos;
            return View(filme);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Filme filme, IFormFile imagem)
        {
            if(ModelState.IsValid)
            {
             if(imagem !=null)
             {
                 string diretorio = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                 string nomeImagem = Guid.NewGuid().ToString() + imagem.FileName;
                 using(FileStream fileStream = new FileStream(Path.Combine(diretorio, nomeImagem), FileMode.Create))
                 {
                    await imagem.CopyToAsync(fileStream);
                    filme.Imagem = "~/img/" + nomeImagem;                  
                 }
                 
             }
             else

                filme.Imagem = TempData["ImagemFilme"].ToString();
                _context.Filmes.Update(filme);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));

            }
            return View(filme);
        }       
        public async Task<IActionResult> Delete(int? id)
        {            
            var filme = await _context.Filmes.FirstOrDefaultAsync(m => m.FilmeId == id);           
            return View(filme);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         public  async Task<IActionResult> DeleteConfirmed(int? id)
        {
             var filme = await _context.Filmes.FindAsync(id);
            _context.Filmes.Remove(filme);           
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));           
            
        }   
        public async Task<IActionResult> Details(int? id)
        {        
            var imagens = _context.Filmes.Select(m => m.FilmeId).ToList();
            var filme =await _context.Filmes.Include(i => i.Genero).OrderBy(c => c.Nome).FirstOrDefaultAsync(m => m.FilmeId == id);         
// 
          return View(filme);
        }     
    }
}