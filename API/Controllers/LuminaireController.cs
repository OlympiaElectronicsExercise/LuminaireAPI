using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Luminaire
{
    public class LuminaireController : BaseApiController
    {
        private readonly ILogger<LuminaireController> _logger;
        private readonly DatabaseContext _context;
        public LuminaireController(ILogger<LuminaireController> logger, DatabaseContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<LuminareModel>>> GetAll()
        {
            var luminaires = await _context.Luminaires.ToListAsync();
            
            if (luminaires.Any()) return Ok(luminaires);

            return NoContent();
        }

        [HttpDelete("{uid}")]
        public async Task<ActionResult> DeleteLuminaire(int uid)
        {
            var luminaire = await _context.Luminaires.FirstOrDefaultAsync(x => x.UID ==  uid);
            var res = _context.Luminaires.Remove(luminaire);
            return Ok();
        }
    }
}