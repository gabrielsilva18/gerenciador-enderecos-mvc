using System.Text;
using GerenciadorEnderecos.Data;
using GerenciadorEnderecos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorEnderecos.Controllers
{
    public class EnderecoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnderecoController(ApplicationDbContext context)
        {
            _context = context;
        }

        private int? UsuarioLogadoId => HttpContext.Session.GetInt32("UsuarioId");

        private IActionResult? RedirecionarSeNaoAutenticado()
        {
            if (UsuarioLogadoId == null)
                return RedirectToAction("Index", "Login");
            return null;
        }

        public async Task<IActionResult> Index()
        {
            var redirect = RedirecionarSeNaoAutenticado();
            if (redirect != null) return redirect;

            var enderecos = await _context.Enderecos
                .Where(e => e.UsuarioId == UsuarioLogadoId!.Value)
                .OrderBy(e => e.Cidade)
                .ThenBy(e => e.Logradouro)
                .ToListAsync();

            return View(enderecos);
        }

        public async Task<IActionResult> ExportarCsv()
        {
            var redirect = RedirecionarSeNaoAutenticado();
            if (redirect != null) return redirect;

            var enderecos = await _context.Enderecos
                .Where(e => e.UsuarioId == UsuarioLogadoId!.Value)
                .OrderBy(e => e.Cidade)
                .ThenBy(e => e.Logradouro)
                .ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("CEP,Logradouro,Complemento,Bairro,Cidade,UF,Número");

            foreach (var e in enderecos)
            {
                sb.Append(CelulaCsv(e.Cep)).Append(',');
                sb.Append(CelulaCsv(e.Logradouro)).Append(',');
                sb.Append(CelulaCsv(e.Complemento)).Append(',');
                sb.Append(CelulaCsv(e.Bairro)).Append(',');
                sb.Append(CelulaCsv(e.Cidade)).Append(',');
                sb.Append(CelulaCsv(e.Uf)).Append(',');
                sb.Append(CelulaCsv(e.Numero));
                sb.AppendLine();
            }

            var encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
            var bytes = encoding.GetBytes(sb.ToString());

            return File(bytes, "text/csv; charset=utf-8", "enderecos.csv");
        }

        private static string CelulaCsv(string? valor)
        {
            var s = valor ?? string.Empty;
            if (s.Contains('"', StringComparison.Ordinal))
                s = s.Replace("\"", "\"\"", StringComparison.Ordinal);
            if (s.Contains(',', StringComparison.Ordinal) || s.Contains('"', StringComparison.Ordinal)
                || s.Contains('\r', StringComparison.Ordinal) || s.Contains('\n', StringComparison.Ordinal))
                return $"\"{s}\"";
            return s;
        }

        public async Task<IActionResult> Details(int? id)
        {
            var redirect = RedirecionarSeNaoAutenticado();
            if (redirect != null) return redirect;

            if (id == null)
                return NotFound();

            var endereco = await _context.Enderecos
                .FirstOrDefaultAsync(m => m.Id == id && m.UsuarioId == UsuarioLogadoId!.Value);

            if (endereco == null)
                return NotFound();

            return View(endereco);
        }

        public IActionResult Create()
        {
            var redirect = RedirecionarSeNaoAutenticado();
            if (redirect != null) return redirect;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cep,Logradouro,Complemento,Bairro,Cidade,Uf,Numero")] Endereco endereco)
        {
            var redirect = RedirecionarSeNaoAutenticado();
            if (redirect != null) return redirect;

            if (ModelState.IsValid)
            {
                endereco.UsuarioId = UsuarioLogadoId!.Value;
                _context.Add(endereco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(endereco);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var redirect = RedirecionarSeNaoAutenticado();
            if (redirect != null) return redirect;

            if (id == null)
                return NotFound();

            var endereco = await _context.Enderecos
                .FirstOrDefaultAsync(e => e.Id == id && e.UsuarioId == UsuarioLogadoId!.Value);

            if (endereco == null)
                return NotFound();

            return View(endereco);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cep,Logradouro,Complemento,Bairro,Cidade,Uf,Numero")] Endereco endereco)
        {
            var redirect = RedirecionarSeNaoAutenticado();
            if (redirect != null) return redirect;

            if (id != endereco.Id)
                return NotFound();

            var existente = await _context.Enderecos
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id && e.UsuarioId == UsuarioLogadoId!.Value);

            if (existente == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                endereco.UsuarioId = UsuarioLogadoId!.Value;
                try
                {
                    _context.Update(endereco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await EnderecoExistsAsync(endereco.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(endereco);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var redirect = RedirecionarSeNaoAutenticado();
            if (redirect != null) return redirect;

            if (id == null)
                return NotFound();

            var endereco = await _context.Enderecos
                .FirstOrDefaultAsync(m => m.Id == id && m.UsuarioId == UsuarioLogadoId!.Value);

            if (endereco == null)
                return NotFound();

            return View(endereco);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var redirect = RedirecionarSeNaoAutenticado();
            if (redirect != null) return redirect;

            var endereco = await _context.Enderecos
                .FirstOrDefaultAsync(e => e.Id == id && e.UsuarioId == UsuarioLogadoId!.Value);

            if (endereco != null)
            {
                _context.Enderecos.Remove(endereco);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EnderecoExistsAsync(int id)
        {
            return await _context.Enderecos.AnyAsync(e => e.Id == id && e.UsuarioId == UsuarioLogadoId!.Value);
        }
    }
}
