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
    public class SessaoController : Controller
    {
        private readonly Conexao _context;

        public SessaoController(Conexao context)
        {
            _context = context;
        }

        // GET: Sessao
        public async Task<IActionResult> Index()
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login");

            return _context.SessaoModel != null ? 
                        View(await _context.SessaoModel.ToListAsync()) :
                        Problem("Entity set 'Conexao.SessaoModel'  is null.");
        }

        // POST: Sessao
        [HttpPost]
        public async Task<IActionResult> Index(String? Nome, String? Local, String? Data)
        {          
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login");

            var lista= from x in _context.SessaoModel select x; 

            if(Nome!=null) lista= lista.Where(y=>y.evento == Nome);
            if(Local!=null) lista= lista.Where(y=>y.local == Local);
            if(Data!=null) lista= lista.Where(y=>y.data == Data);

            return lista != null ? 
                    View(await lista.ToListAsync()) :
                    Problem("Lista vazia");
        }

        //"POST" Compra
        public async Task<IActionResult> Buy(int? id){

            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login");

            if (id == null || _context.SessaoModel == null)
            {
                return NotFound();
            }

            var sessaoModel = await _context.SessaoModel.FindAsync(id);
            if (sessaoModel == null)
            {
                return NotFound();
            }

            if(sessaoModel.assentos> 0){
                bool val= ValidacaoData(sessaoModel);
                if(val!=true) return View(sessaoModel);

                return Redirect("https://localhost:7145/Comprar/"+sessaoModel.Id);
                
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Sessao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login");

            if (id == null || _context.SessaoModel == null)
            {
                return NotFound();
            }

            var sessaoModel = await _context.SessaoModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sessaoModel == null)
            {
                return NotFound();
            }

            return View(sessaoModel);
        }

        // GET: Sessao/Create
        public IActionResult Create()
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login");

            return View();
        }

        // POST: Sessao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,evento,local,data,hr_inicio,hr_fim,preco,assentos")] SessaoModel sessaoModel)
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login");

            if (ModelState.IsValid)
            {
                bool val= ValidacaoData(sessaoModel);
                if(val!=true) return View(sessaoModel);

                val = ValidacaoSalvos(sessaoModel);
                if(val!=true) return View(sessaoModel);

                _context.Add(sessaoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sessaoModel);
        }

        // GET: Sessao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login");

            if (id == null || _context.SessaoModel == null)
            {
                return NotFound();
            }

            var sessaoModel = await _context.SessaoModel.FindAsync(id);
            if (sessaoModel == null)
            {
                return NotFound();
            }
            return View(sessaoModel);
        }

        // POST: Sessao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,evento,local,data,hr_inicio,hr_fim,preco,assentos")] SessaoModel sessaoModel)
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login");

            if (id != sessaoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool val= ValidacaoData(sessaoModel);
                if(val!=true) return View(sessaoModel);

                try
                {
                    _context.Update(sessaoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessaoModelExists(sessaoModel.Id))
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
            return View(sessaoModel);
        }

        // GET: Sessao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login");
            
            if (id == null || _context.SessaoModel == null)
            {
                return NotFound();
            }

            var sessaoModel = await _context.SessaoModel
                .FirstOrDefaultAsync(m => m.Id == id);

            if (sessaoModel == null)
            {
                return NotFound();
            }

            bool val= ValidacaoData(sessaoModel);
            if(val!=true||sessaoModel.comprados!=0) return RedirectToAction(nameof(Index));

            return View(sessaoModel);
        }

        // POST: Sessao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login");

            if (_context.SessaoModel == null)
            {
                return Problem("Entity set 'Conexao.SessaoModel'  is null.");
            }
            var sessaoModel = await _context.SessaoModel.FindAsync(id);
            bool val= ValidacaoData(sessaoModel);
            if(val!=true||sessaoModel.comprados!=0) return RedirectToAction(nameof(Index));

            if (sessaoModel != null)
            {
                _context.SessaoModel.Remove(sessaoModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessaoModelExists(int id)
        {
          return (_context.SessaoModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool CheckSes(){
            var log= HttpContext.Session.GetString("ses");
            if(log == null) return false;
            else if(log.Equals("Adm")||log.Equals("Usr")) return true;
            return false;
        }

        private bool ValidacaoData(SessaoModel sessaoModel){
            
            if(sessaoModel.data==null||sessaoModel.hr_fim==null||sessaoModel.hr_inicio==null) return false;

            #region validacao_campos
            var aux= sessaoModel.hr_inicio.Replace(":",",");
            var hrInicio= Convert.ToDouble(aux);

            aux= sessaoModel.hr_fim.Replace(":",",");
            var hrFim= Convert.ToDouble(aux);

            aux= DateTime.Now.ToShortTimeString();
            var a= aux.Replace(":",",");
            var hrAgora= Convert.ToDouble(a);

            aux= sessaoModel.data.Replace("-","");
            var dtInicio= Convert.ToInt32(aux);

            aux= DateTime.Now.ToShortDateString();
            DateTime date = DateTime.ParseExact(aux, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var formattedDate = date.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            
            var dtAgora= Convert.ToInt32(formattedDate);
            #endregion

            if(hrInicio<hrFim && dtInicio>=dtAgora) 
                if(dtInicio == dtAgora && hrInicio<hrAgora)return false;
                else return true;
            return false;
        }
    
        private bool ValidacaoSalvos(SessaoModel sessaoModel){

            #region validacao_salvos
            var lista= from x in _context.SessaoModel select x;
            var a= sessaoModel.hr_inicio;
            var b= sessaoModel.hr_fim;

            foreach(var item in lista.ToList()){
                if(item.data.Equals(sessaoModel.data))
                if(item.local.Equals(sessaoModel.local)){ //no mesmo local e data...

                    var aux2= item.hr_fim.Replace(":",",");
                    var c= Convert.ToDouble(aux2);
                    aux2= item.hr_inicio.Replace(":",",");
                    var d= Convert.ToDouble(aux2);

                    if(c>Convert.ToDouble(a.Replace(":",","))) 
                    if(d<Convert.ToDouble(a.Replace(":",","))) return false; //inicio da nova está antes de terminar a já existente

                    if(c>Convert.ToDouble(b.Replace(":",",")))
                    if(d<Convert.ToDouble(b.Replace(":",","))) return false; //fim da nova está antes de terminar a já existente
                }
            }
            #endregion
            return true;
        }
    }
}
