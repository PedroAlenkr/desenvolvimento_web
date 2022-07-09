using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using System.Web;

namespace mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Conexao _context;

    public HomeController(ILogger<HomeController> logger, Conexao context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var log= HttpContext.Session.GetString("ses");
        if(log == null) return RedirectToAction(nameof(Login));
        if(log.Equals("Adm")||log.Equals("Usr")) return View();
        return RedirectToAction(nameof(Login));
    }
    public IActionResult Login()
    {
        ViewBag.var= 0;
        return View();
    }

    public IActionResult Cadastro()
    {
        ViewBag.var= 0;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cadastro(UserModel userModel)
    {
        var lista= from x in _context.Users where x.email == userModel.email select x;
        foreach (var item in lista)
            if(true){
                ViewBag.var=0;
                return View(userModel);
            }  
        if(userModel.senha==null){
            ViewBag.var=0;
            return View(userModel);
        }
        var a= string.Concat(userModel.senha.Reverse());
        var b= System.Text.Encoding.UTF8.GetBytes(a);
        var c= System.Convert.ToBase64String(b);

        userModel.senha=c;

        _context.Add(userModel);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("ses");
        HttpContext.Session.Remove("id");
        return(RedirectToAction(nameof(Login)));
    }

    [HttpPost]
    public IActionResult Login(String? email, String? senha)
    {
        
        if(email!=null && senha!=null){
            var lista= from x in _context.Users select x; 

            var a= string.Concat(senha.Reverse());
            var b= System.Text.Encoding.UTF8.GetBytes(a);
            var c= System.Convert.ToBase64String(b);

            if(email!=null) lista= lista.Where(y=>y.email == email);
            if(senha!=null) lista= lista.Where(y=>y.senha == c);


            if(lista!=null && lista.Any(y=>y.tipo == "Admin")) {
                HttpContext.Session.SetString("ses","Adm");
                HttpContext.Session.SetInt32("id",lista.First().Id);
            }
            else if(lista!=null && lista.Any(y=>y.tipo == "Usuario")){
                HttpContext.Session.SetString("ses","Usr");
                HttpContext.Session.SetInt32("id",lista.First().Id);
            }

            var log= HttpContext.Session.GetString("ses");
            if(log==null) {ViewBag.var=0; return View();}
            else if(log.Equals("Adm")||log.Equals("Usr")) return RedirectToAction(nameof(Index));
        }
        
        ViewBag.var=0;
        return View();    
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
