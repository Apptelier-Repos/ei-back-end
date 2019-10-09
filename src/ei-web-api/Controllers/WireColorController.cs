using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ei_infrastructure.Data.Queries;
using ei_web_api.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ei_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WireColorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public WireColorController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET: api/WireColor
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetAllWireColors.Query());
            if (response.Any())
                return Ok(_mapper.Map<IEnumerable<WireColorViewModel>>(response));
            return NoContent();
        }

        // GET: api/WireColor/B
        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code)
        {
            var response = await _mediator.Send(new GetAWireColorByCode.Query(code));
            return Ok(_mapper.Map<WireColorViewModel>(response));
        }
    }
}