using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToggleSystem.Api.Models.Response;
using ToggleSystem.Domain.Interfaces.Services;

namespace ToggleSystem.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ToggleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IToggleService _toggleService;

        public ToggleController(IMapper mapper,
                                IToggleService toggleService)
        {
            _mapper = mapper;
            _toggleService = toggleService;
        }

        [HttpGet("{client}/{toggleVersion}")]
        [Authorize(Policy = "CanGetToggle")]
        public async Task<ActionResult<IEnumerable<ToggleResponse>>> Get(string client, int toggleVersion)
        {
            var toggles = await _toggleService.GetAll(client, toggleVersion);

            if (toggles.Any())
            {
                return Ok(_mapper.Map<IEnumerable<ToggleResponse>>(toggles));
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize(Policy = "CanManage")]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "CanManage")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "CanManage")]
        public void Delete(int id)
        {
        }
    }
}
