using FluentResults;
using Identity.Application.Dtos.Params;

namespace Identity.Application.Interfaces.UseCases;

public interface IRegisterUserUseCase
{
    public Task<Result> ExecuteAsync(UserDto createUserDto);
}
