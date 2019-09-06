using System.Linq;
using System.Threading.Tasks;
using ei_core.Exceptions;
using ei_infrastructure.Data.Commands;
using ei_infrastructure.Data.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ei_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/UserAccount
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetAllUserAccounts.Query());
            if (response.Any())
                return Ok(response);
            return NoContent();
        }

        // GET: api/UserAccount/mota
        [HttpGet("{username}", Name = "Get")]
        public async Task<IActionResult> Get(string username)
        {
            var response = await _mediator.Send(new GetAUserAccountByUsername.Query
                {Username = username});
            return Ok(response);
        }

        // PUT: api/UserAccount/xyz
        [HttpPut("{username}")]
        public async Task<IActionResult> Put(string username, [FromBody] string password)
        {
            try
            {
                var response =
                    await _mediator.Send(new CreateUserAccount.Command {Username = username, Password = password});
                return Ok(response);
            }
            catch (UsernameAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
        }
    }
}