using App.Application.Commands.Auth;
using App.Domain.DTOs.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(ISender sender) : ControllerBase
    {

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await sender.Send(new SignUpCommand(request));
            if (!result.IsSuccessful) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInDto request)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var result = await sender.Send(new SignInCommand(request));
            if (!result.IsSuccessful) return BadRequest(result);
            return Ok(result);
        }
    }
}
