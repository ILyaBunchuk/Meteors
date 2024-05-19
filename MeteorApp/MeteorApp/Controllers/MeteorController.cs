using MeteorApp.Interfaces;
using MeteorApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeteorApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeteorController : ControllerBase
    {
        private readonly IMeteorService _meteorService;

        public MeteorController(IMeteorService meteorService)
        {
            _meteorService = meteorService;
        }

        [HttpGet]
        [Route("filter")]
        public async Task<FilterMeteorsView> GetFilterAsync()
        {
            return await _meteorService.GetFilterAsync();
        }

        [HttpGet]
        [Route("meteorsTotal")]
        public async Task<MeteorsTotalView> GetMeteorsTotal([FromQuery]FilterMeteorsDTO filterMeteorsDTO)
        {
            return await _meteorService.GetMeteorsTotalAsync(filterMeteorsDTO);
        }

        [HttpGet]
        [Route("meteors")]
        public async Task<IEnumerable<MeteorView>> GetMeteors([FromQuery] FilterMeteorsPaginationDTO filterMeteorsPaginationDTO)
        {
            return await _meteorService.GetMeteorsAsync(filterMeteorsPaginationDTO);
        }
    }
}
