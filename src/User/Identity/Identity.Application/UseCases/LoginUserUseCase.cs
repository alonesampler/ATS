using FluentResults;
using Identity.Application.Dtos;
using Identity.Application.Interfaces.Producer;
using Identity.Domain.Entities;
using Identity.Domain.Events;
using Identity.Domain.Interfaces;
using Identity.Domain.ValueObjects;

namespace Identity.Application.UseCases;

public class LoginUserUseCase(
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher,
    IProducer<UserLoginEvent> producer)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IProducer<UserLoginEvent> _producer = producer;

    public async Task<Result<Guid>> ExecuteAsync(UserLoginDto dto)
    {
        var isEmail = dto.Login.Contains('@');

        var user = isEmail
            ? await _unitOfWork.UserRepository.GetByEmailAsync(dto.Login)
            : await _unitOfWork.UserRepository.GetByPhoneAsync(dto.Login);

        if (user is null)
            return Result.Fail(ErrorMessage.UserNotFound);

        if (!_passwordHasher.VerifyPassword(dto.Password, user.PasswordHash))
            return Result.Fail(ErrorMessage.InvalidPassword);

        var confirmationCode = user.EmailConfirmationCode;

        if (confirmationCode.Code is null)
            confirmationCode = VereficationCode.Create();

        var sesion = Session.Create(
            Guid.NewGuid(),
            user.Id,
            DateTime.UtcNow.AddHours(12),
            "Late",
            "Late");


    }
}
