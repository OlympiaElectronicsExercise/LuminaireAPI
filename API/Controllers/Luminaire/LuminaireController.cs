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

        [HttpGet("{uid}")]
        public async Task<ActionResult<LuminareModel>> GetById(int uid)
        {
            var entity = await _context.Luminaires.FirstOrDefaultAsync(x => x.UID == uid);
            if (entity is not null) return Ok(entity);
            return NotFound();
        }

        [HttpDelete("{uid}")]
        public async Task<ActionResult> DeleteLuminaire(int uid)
        {
            var luminaire = await _context.Luminaires.FirstOrDefaultAsync(x => x.UID ==  uid);
            if (luminaire is null) return NotFound();
            var res = _context.Luminaires.Remove(luminaire);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<LuminareModel>> CreateOrUpdate(LuminareModel luminare)
        {
            var entity = await _context.Luminaires.FirstOrDefaultAsync(x => x.Address == luminare.Address);

            if (entity is not null)
            {
                luminare.CreatedOn = entity.CreatedOn;
                luminare.UpdateOn = DateTime.Now;
                var updatedEntity = _context.Luminaires.Update(luminare);
                await _context.SaveChangesAsync();
                return Ok(updatedEntity);
            }
            else
            {
                await _context.Luminaires.AddAsync(luminare);
                await _context.SaveChangesAsync();
                return Ok(luminare);
            }
        }
    }
}