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
        public async Task<FilterMeteorsView> GetFilterDataAsync()
        {
            return await _meteorService.GetFilterDataAsync();
        }

        [HttpGet]
        [Route("meteorsTotal")]
        public async Task<MeteorsTotalDataView> GetMeteorsTotalData([FromQuery]FilterMeteorsDTO filterMeteorsDTO)
        {
            return await _meteorService.GetMeteorsTotalDataAsync(filterMeteorsDTO);
        }

        [HttpGet]
        [Route("meteors")]
        public async Task<IEnumerable<MeteorView>> GetMeteors([FromQuery] FilterMeteorsPaginationDTO filterMeteorsPaginationDTO)
        {
            return await _meteorService.GetMeteorsAsync(filterMeteorsPaginationDTO);
        }
    }
}
