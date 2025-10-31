using FluentResults;
using Identity.Application.Dtos.Request;

namespace Identity.Application.Interfaces.UseCases;

public interface IRegisterUserUseCase
{
    public Task<Result> ExecuteAsync(UserDto createUserDto);
}
