using FindMyPark.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FindMyPark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly IAIService _aiService;

        public AIController(IAIService aiService)
        {
            _aiService = aiService;
        }

        [HttpGet("prediction")]
        public async Task<ActionResult<List<OccupancyForecast>>> GetPrediction(int listingId, DateTime date)
        {
            var forecast = await _aiService.PredictOccupancyAsync(listingId, date);
            return Ok(forecast);
        }
    }
}
