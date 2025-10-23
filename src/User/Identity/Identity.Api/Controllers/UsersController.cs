using Identity.Application.Dtos.Create;
using Identity.Application.Interfaces.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> RegisterUser(CreateUserDto dto,
        [FromServices] IRegisterUserUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(dto);

        if (result.IsFailed)
            return BadRequest(result.Errors);

        return Ok(result);
    }
}
