using Finance.API.Application.Requests;
using Finance.API.Application.Responses;
using Finance.API.Application.Services;
using Finance.API.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService): ControllerBase
    {
        private readonly IAuthService _authService = authService;


        [HttpPost("/Register")]
        public async Task<IActionResult> Register(CreateAccountRequest request, [FromServices] IValidator<CreateAccountRequest> validator)
        {
            request.Validate(validator);
            await _authService.CreateAccount(request);

            return Ok(new Response<string>("account register with success, try to login",201));
        }

        [HttpPost("/Login")]
        public async Task<IActionResult> Login(LoginModelRequest request)
        {
           var token =  await _authService.Login(request);

            return Ok(new Response<string>(token,200));
        }

    }
}
