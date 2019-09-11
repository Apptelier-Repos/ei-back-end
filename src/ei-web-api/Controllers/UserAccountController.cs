using System;
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
            var response = await _mediator.Send(new GetAUserAccountByUsername.Query(username));
            return Ok(response);
        }

        // POST: api/UserAccount/authenticate
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] DTOs.UserCredentials userCredentials)
        {
            // TODO: Replace this rudimentary user authentication with feature #161 (https://dev.azure.com/Apptelier/Entrenamiento%20Imaginativo/_workitems/edit/161).
            bool authenticationSucceeded;
            try
            {
                var userAccount = await _mediator.Send(new GetAUserAccountByUsername.Query(userCredentials.Username));
                authenticationSucceeded =
                    userAccount?.PasswordMatches(userCredentials.Password) ?? false;
            }
            catch (Exception e) when (e is ArgumentException || e is ArgumentNullException)
            {
                return BadRequest(e.Message);
            }
            if (authenticationSucceeded)
                return Ok();
            return Unauthorized();
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