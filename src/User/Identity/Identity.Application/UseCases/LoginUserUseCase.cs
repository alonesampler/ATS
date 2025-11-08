using FluentResults;
using Identity.Application.Dtos.Params;
using Identity.Application.Interfaces.Producer;
using Identity.Domain.Events;
using Identity.Domain.Interfaces;

namespace Identity.Application.UseCases;

public class LoginUserUseCase(
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher,
    IProducer<UserLoginEvent> producer)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IProducer<UserLoginEvent> _producer = producer;

    public async Task<Result<Guid>> ExecuteAsync(UserDto dto)
    {

    }
}
