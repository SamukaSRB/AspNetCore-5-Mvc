using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmsJust.Models;

namespace FilmsJust.Controllers
{
    public class ClienteController : Controller
    {
        private readonly Context _context;   
        public ClienteController(Context context)
        {
            _context = context;

        }    
        // Chamando as view de listagem de clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }
        // Chamando o formulario de cadastro
        public IActionResult Create()
        {
            return View();
        }
        // Inserindo no banco os dados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Endereco,Cep,Cidade,Estado,Telefone,Email,Cpf")]Cliente cliente)
        {
            if(ModelState.IsValid)
            {
                cliente.Id = Guid.NewGuid();
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);


        }
        //Chamando a view de Edição de clientes
        public async Task<IActionResult> Edit(Guid? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }
        // Editando registros de clientes
        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Endereco,Cep,Cidade,Estado,Telefone,Email,Cpf")]Cliente cliente)
         {
             if(id !=cliente.Id)
             {
                 return NotFound();
             }
             if(ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(cliente);
                     await _context.SaveChangesAsync();
                  }
                  catch(DbUpdateConcurrencyException)
                  {
                      if(!ClienteExists(cliente.Id))
                      {
                          return NotFound();
                      }
                      else
                      {
                          throw;
                      }
                  }
                  return RedirectToAction(nameof(Index));
                }
                  return View(cliente);
              }
         //Chamando a view responsavel por eliminar registro
        public async Task<IActionResult> Delete(Guid? id)
        {
            if(id == null)
            {
                return NotFound();

            }
            var cliente = await _context.Clientes.FirstOrDefaultAsync(m => m.Id == id);
            if(cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }
        // Chando a ação do botao para apagar um registro
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));           
            
        }        
        // Detalhes dos clientes
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }
        //Filtro
        private bool ClienteExists(Guid id)
        {
            return _context.Clientes.Any(e=> e.Id == id);
        }
    }
    

}