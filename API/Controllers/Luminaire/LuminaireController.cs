namespace API.Controllers.Luminaire
{
    public class LuminaireController : BaseApiController
    {
        private readonly ILogger<LuminaireController> _logger;
        public LuminaireController(ILogger<LuminaireController> logger)
        {
            _logger = logger;
        }
    }
}