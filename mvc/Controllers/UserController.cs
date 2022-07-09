using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc.Models;

namespace mvc.Controllers_
{
    public class UserController : Controller
    {
        private readonly Conexao _context;

        public UserController(Conexao context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login");
  
            var log= HttpContext.Session.GetString("ses");
            if(log.Equals("Usr")) return Redirect("https://localhost:7145/Home/Index");

              return _context.Users != null ? 
                          View(await _context.Users.ToListAsync()) :
                          Problem("Entity set 'Conexao.Users'  is null.");
        }

        // POST: User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(String? Nome, String? Tipo)
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login");
         
            var lista= from x in _context.Users select x; 

            if(Nome!=null) lista= lista.Where(y=>y.nome == Nome);
            if(Tipo!=null) lista= lista.Where(y=>y.tipo == Tipo);

            return lista != null ? 
                    View(await lista.ToListAsync()) :
                    Problem("Lista vazia");
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login");

            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var userModel = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login");

            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,nome,email,senha,nascimento,cpf,sexo,tipo")] UserModel userModel)
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login");

            if (ModelState.IsValid)
            {
                var lista= from x in _context.Users where x.email == userModel.email select x;
                foreach (var item in lista)
                    if(true) return View(userModel); //gamb monstra
                    
                var a= string.Concat(userModel.senha.Reverse());
                var b= System.Text.Encoding.UTF8.GetBytes(a);
                var c= System.Convert.ToBase64String(b);

                userModel.senha=c;

                _context.Add(userModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(userModel);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login"); 
            if(id==null) id= HttpContext.Session.GetInt32("id");

            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var userModel = await _context.Users.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nome,email,senha,nascimento,cpf,sexo,tipo")] UserModel userModel)
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login");

            if (id != userModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                    
                var a= string.Concat(userModel.senha.Reverse());
                var b= System.Text.Encoding.UTF8.GetBytes(a);
                var c= System.Convert.ToBase64String(b);

                userModel.senha=c;

                try
                {
                    _context.Update(userModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserModelExists(userModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if(userModel.tipo.Equals("Admin")) {
                    HttpContext.Session.Remove("ses");
                    HttpContext.Session.SetString("ses","Adm");
                }
                if(userModel.tipo.Equals("Usuario")) {
                    HttpContext.Session.Remove("ses");
                    HttpContext.Session.SetString("ses","Usr");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login");

            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var userModel = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login");

            if (_context.Users == null)
            {
                return Problem("Entity set 'Conexao.Users'  is null.");
            }
            var userModel = await _context.Users.FindAsync(id);
            if (userModel != null)
            {
                _context.Users.Remove(userModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserModelExists(int id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool CheckSes(){
            var log= HttpContext.Session.GetString("ses");
            if(log == null) return false;
            else if(log.Equals("Adm")||log.Equals("Usr")) return true;
            return false;
        }
    }
}
