using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }


        [HttpGet]
        [Route("export-model")]
        public IActionResult ExportModel()
        {
            var csv = new StringBuilder();
            csv.AppendLine("Nome;Descricao");

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());

            return File(bytes, "text/csv", "Usuarios.csv");
        }


        /// <summary>
        /// Adicionar Usuarios em lote
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("batch")]
        public async Task<IActionResult> Add(IFormFile file)
        {
            await _service.Add(file);
            return Ok();
        }
    }
}