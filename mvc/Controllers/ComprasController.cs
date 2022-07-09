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
    public class ComprasController : Controller
    {
        
        private ComprasModel compra= new ComprasModel();
        private readonly Conexao _context;

        public ComprasController(Conexao context)
        {
            _context = context;
        }

        // GET: Compras
        public async Task<IActionResult> Index()
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login"); 

            var log= HttpContext.Session.GetString("ses");
            if(log.Equals("Usr")) return Redirect("https://localhost:7145/Home/Index");

            if(_context.SessaoModel != null) {
                var lista= from x in _context.SessaoModel select x;

                foreach (var item in lista)
                {
                    bool val= ValidacaoData(item);
                    if(val!=true||item.assentos<=0) lista = lista.Where(y=>y.Id!=item.Id);
                }
                
                return View(lista);              
            }

            return Problem("Entity set 'Conexao.SessaoModel'  is null."); 
        }

        //POST: Compras
        [HttpPost]
        public async Task<IActionResult> Index(String? Nome, String? Data)
        {          
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login");

            var log= HttpContext.Session.GetString("ses");
            if(log.Equals("Usr")) return Redirect("https://localhost:7145/Home/Index");


            var lista= from x in _context.SessaoModel select x; 

            if(Nome!=null) lista= lista.Where(y=>y.evento == Nome||y.local == Nome);
            if(Data!=null) lista= lista.Where(y=>y.data == Data);

            return lista != null ? 
                    View(await lista.ToListAsync()) :
                    Problem("Lista vazia");
        }

        // GET: Compras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login"); 

            var log= HttpContext.Session.GetString("ses");
            if(log.Equals("Usr")) return Redirect("https://localhost:7145/Home/Index");

            if (id == null || _context.SessaoModel == null)
            {
                return NotFound();
            }

            var sessaoModel = await _context.SessaoModel
                .FirstOrDefaultAsync(m=>m.Id == id);

            if (sessaoModel == null)
            {
                return NotFound();
            }

            var lista= from x in _context.ComprasModel where x.id_sessao == id select x; //isso é mt bom
            ViewBag.denero=0;
            foreach (var item in lista)
            {
                ViewBag.denero+= item.preco_total;
            }

            return View(sessaoModel);
        }

        // GET: Compras/Create
       [Route("Comprar/{idSessao}")]
        public async Task<IActionResult> Create(int idSessao)
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login"); 

            var id= HttpContext.Session.GetInt32("id");
            var sessaoModel = await _context.SessaoModel.FindAsync(idSessao);
            var userModel = await _context.Users.FindAsync(id);

            if(userModel.cpf==null||userModel.nascimento==null||userModel.sexo==null)
                return Redirect("https://localhost:7145/User/Edit/"+id);

            compra.cpf= userModel.cpf;
            compra.dt_compra= DateTime.Now.ToString("yyyy-MM-dd");
            compra.dt_evento= sessaoModel.data;
            compra.evento= sessaoModel.evento;
            compra.id_sessao= sessaoModel.Id;

            return View(compra);
        }

        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirma([Bind("Id,evento,ingressos,dt_compra,dt_evento,cpf,pagamento,parcelas,preco_total,id_sessao")] ComprasModel comprasModel)
        {
            if(!CheckSes()) return Redirect("https://localhost:7145/Home/Login"); 

            if (ModelState.IsValid)
            {
                var sessaoModel = await _context.SessaoModel.FindAsync(comprasModel.id_sessao);
                
                if(sessaoModel.assentos>0 && (sessaoModel.assentos-comprasModel.ingressos)>=0)
                {
                    comprasModel.preco_total= (comprasModel.ingressos * sessaoModel.preco)*1.05;
                    ViewBag.parcelas= comprasModel.preco_total/comprasModel.parcelas; //litlle gamb
                    sessaoModel.assentos-=comprasModel.ingressos;  
                    sessaoModel.comprados+=comprasModel.ingressos;

                    _context.Add(comprasModel);
                    _context.Update(sessaoModel);
                    await _context.SaveChangesAsync();
                    return View(comprasModel);
                }
            }
            return Redirect("https://localhost:7145/Comprar/"+comprasModel.id_sessao);
        }
  
        private bool ComprasModelExists(int id)
        {
          return (_context.ComprasModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool ValidacaoData(SessaoModel sessaoModel){
            
            //if(sessaoModel==null||sessaoModel.hr_fim==null||sessaoModel.hr_inicio==null) return false;

            #region validacao_campos
            var aux= sessaoModel.data.Replace("-","");
            var dtInicio= Convert.ToInt32(aux);

            aux= DateTime.Now.ToShortDateString();
            DateTime date = DateTime.ParseExact(aux, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var formattedDate = date.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            
            var dtAgora= Convert.ToInt32(formattedDate);
            #endregion

            if(dtInicio>=dtAgora) return true;
            return false;
        }

        private bool CheckSes(){
            var log= HttpContext.Session.GetString("ses");
            if(log == null) return false;
            else if(log.Equals("Adm")||log.Equals("Usr")) return true;
            return false;
        }
    }
}
