using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MeuSiteEmMVC.Data;
using MeuSiteEmMVC.Models;
using MeuSiteEmMVC.Services;
using MeuSiteEmMVC.ViewModels;
using System.Security.Claims;

namespace MeuSiteEmMVC.Controllers
{
    [Authorize]
    public class EnderecoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ViaCepService _viaCepService;

        public EnderecoController(AppDbContext context, ViaCepService viaCepService)
        {
            _context = context;
            _viaCepService = viaCepService;
        }

        public IActionResult Index()
        {
            var usuarioId = int.Parse(User.FindFirstValue("UsuarioId"));
            var enderecos = _context.Enderecos
                .Where(e => e.UsuarioId == usuarioId)
                .ToList();
            return View(enderecos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(EnderecoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuarioId = int.Parse(User.FindFirstValue("UsuarioId"));
            var endereco = new Endereco
            {
                Cep = model.Cep,
                Logradouro = model.Logradouro,
                Complemento = model.Complemento,
                Bairro = model.Bairro,
                Cidade = model.Cidade,
                Uf = model.Uf,
                Numero = model.Numero,
                UsuarioId = usuarioId
            };
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            var usuarioId = int.Parse(User.FindFirstValue("UsuarioId"));
            var endereco = _context.Enderecos
                .FirstOrDefault(e => e.Id == id && e.UsuarioId == usuarioId);

            if (endereco == null)
                return NotFound();

            var model = new EnderecoViewModel
            {
                Id = endereco.Id,
                Cep = endereco.Cep,
                Logradouro = endereco.Logradouro,
                Complemento = endereco.Complemento,
                Bairro = endereco.Bairro,
                Cidade = endereco.Cidade,
                Uf = endereco.Uf,
                Numero = endereco.Numero
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Editar(EnderecoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuarioId = int.Parse(User.FindFirstValue("UsuarioId"));
            var endereco = _context.Enderecos
                .FirstOrDefault(e => e.Id == model.Id && e.UsuarioId == usuarioId);

            if (endereco == null)
                return NotFound();

            endereco.Cep = model.Cep;
            endereco.Logradouro = model.Logradouro;
            endereco.Complemento = model.Complemento;
            endereco.Bairro = model.Bairro;
            endereco.Cidade = model.Cidade;
            endereco.Uf = model.Uf;
            endereco.Numero = model.Numero;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Deletar(int id)
        {
            var usuarioId = int.Parse(User.FindFirstValue("UsuarioId"));
            var endereco = _context.Enderecos
                .FirstOrDefault(e => e.Id == id && e.UsuarioId == usuarioId);

            if (endereco == null)
                return NotFound();

            return View(endereco);
        }

        [HttpPost, ActionName("Deletar")]
        public IActionResult DeletarConfirmado(int id)
        {
            var usuarioId = int.Parse(User.FindFirstValue("UsuarioId"));
            var endereco = _context.Enderecos
                .FirstOrDefault(e => e.Id == id && e.UsuarioId == usuarioId);

            if (endereco == null)
                return NotFound();

            _context.Enderecos.Remove(endereco);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Buscar CEP via ViaCEP
        [HttpGet]
        public async Task<IActionResult> BuscarCep(string cep)
        {
            var resultado = await _viaCepService.BuscarCepAsync(cep);

            if (resultado == null || resultado.Erro)
                return Json(new { erro = true });

            return Json(new
            {
                logradouro = resultado.Logradouro,
                complemento = resultado.Complemento,
                bairro = resultado.Bairro,
                cidade = resultado.Localidade,
                uf = resultado.Uf
            });
        }

        // GET: Para Baixar em CSV
        public IActionResult ExportarCsv()
        {
            var usuarioId = int.Parse(User.FindFirstValue("UsuarioId"));
            var enderecos = _context.Enderecos
                .Where(e => e.UsuarioId == usuarioId)
                .ToList();

            var csv = new System.Text.StringBuilder();
            csv.AppendLine("CEP, Logradouro, Complemento, Bairro, Cidade, UF, Numero");


            foreach (var e in enderecos)
            {
                csv.AppendLine($"{e.Cep},{e.Logradouro},{e.Complemento},{e.Bairro},{e.Cidade},{e.Uf},{e.Numero}");

            }

            var bytes = System.Text.Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", "enderecos.csv");

        }
    }

}
