using MEO_XPTO.Data;
using MEO_XPTO.Models.Business;
using MEO_XPTO.Models.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MEO_XPTO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class XptoFormularioController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<XptoFormularioController> _logger;

        public XptoFormularioController(ILogger<XptoFormularioController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public IEnumerable<Formulario> Get()
        {
            return _db.Formularios;
        }

        [HttpGet("{itemType:int}")]
        public Formulario Get(int formularioId)
        {
            Formulario formulario = _db.Formularios.Find(formularioId);

            if (formulario == null)
            {
                _logger.Log(LogLevel.Error, "O formulário não foi encontrado.");
                throw new Exception("O formulário não foi encontrado.");
            }

            return formulario;
        }

        [HttpPost]
        public IActionResult Post([FromBody] FormularioViewModel formulario)
        {
            try
            {
                if (_db.Formularios.Where(item => item.Email == formulario.Email).Count() == 0)
                {
                    Formulario novoFormulario = new Formulario(formulario.FirstName, formulario.LastName,
                        formulario.Email,
                        formulario.PhoneNumber, formulario.IsSubscribedToNewsletter);

                    _db.Formularios.Add(novoFormulario);
                    _db.SaveChanges();
                }
                else
                {
                    _logger.Log(LogLevel.Error, "Cannot create multiple accounts using the same email.");
                    throw new Exception("Cannot create multiple accounts using the same email.");
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);
                throw new Exception(ex.Message);
            }

            return Ok();
        }
    }
}