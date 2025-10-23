using FluentResults;
using Identity.Application.Dtos.Create;

namespace Identity.Application.Interfaces.UseCases;

public interface IRegisterUserUseCase
{
    public Task<Result> ExecuteAsync(CreateUserDto createUserDto);
}
