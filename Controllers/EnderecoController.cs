using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MeuSiteEmMVC.Data;
using MeuSiteEmMVC.Models;
using MeuSiteEmMVC.ViewModels;
using System.Security.Claims;

namespace MeuSiteEmMVC.Controllers
{
    [Authorize]
    public class EnderecoController : Controller
    {
        private readonly AppDbContext _context;

        public EnderecoController(AppDbContext context)
        {
            _context = context;
        }

        // Lista os endereços do usuário logado
        public IActionResult Index()
        {
            var usuarioId = int.Parse(User.FindFirstValue("UsuarioId"));
            var enderecos = _context.Enderecos
                .Where(e => e.UsuarioId == usuarioId)
                .ToList();

            return View(enderecos);
        }

        // GET: Criar novo endereço
        public IActionResult Criar()
        {
            return View();
        }

        // POST: Salvar novo endereço
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

        // GET: Editar endereço
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

        // POST: Salvar edição
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

        // GET: Confirmar exclusão
        public IActionResult Deletar(int id)
        {
            var usuarioId = int.Parse(User.FindFirstValue("UsuarioId"));
            var endereco = _context.Enderecos
                .FirstOrDefault(e => e.Id == id && e.UsuarioId == usuarioId);

            if (endereco == null)
                return NotFound();

            return View(endereco);
        }

        // POST: Confirmar exclusão
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
    }
}