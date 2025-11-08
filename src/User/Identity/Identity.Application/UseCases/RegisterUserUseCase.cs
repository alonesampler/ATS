using FluentResults;
using Identity.Application.Dtos.Params;
using Identity.Application.Interfaces.Producer;
using Identity.Application.Interfaces.UseCases;
using Identity.Domain.Entities;
using Identity.Domain.Events;
using Identity.Domain.Interfaces;
using Identity.Domain.ValueObjects;

namespace Identity.Application.UseCases;

public class RegisterUserUseCase(
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork,
    IProducer<UserRegisteredEvent> producer) : IRegisterUserUseCase
{
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProducer<UserRegisteredEvent> _producer = producer;

    public async Task<Result> ExecuteAsync(UserDto dto)
    {
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(dto.Email);
        
        if (user is not  null)
            return Result.Fail(ErrorMessage.EmailAlreadyExists);

        var newUser = CreateUserFromDto(dto);

        await _unitOfWork.BeginTransactionAsync();
        await _unitOfWork.UserRepository.AddAsync(newUser);
        await _unitOfWork.CommitTransactionAsync();

        var message = new UserRegisteredEvent(
            newUser.Id,
            newUser.Email.Value,
            dto.FullName.FirstName,
            dto.FullName.LastName,
            dto.FullName.MiddleName,
            newUser.EmailConfirmationCode
            );

        await _producer.ProduceAsync(message);

        return Result.Ok();
    }

    private User CreateUserFromDto(UserDto dto)
    {
        var userId = Guid.NewGuid();

        var email = new Email(dto.Email);

        var phoneNumber = dto.PhoneNumber != null
            ? new PhoneNumber(dto.PhoneNumber.CountryCode, dto.PhoneNumber.Number)
            : null;

        var hashedPassword = _passwordHasher.HashPassword(dto.Password);
        var passwordHash = new PasswordHash(hashedPassword);

        var registeredAt = DateTime.UtcNow;
        var isEmailConfirmed = false;
        var confirmationCode = VereficationCode.Create();

        return User.Register(
            userId,
            email,
            phoneNumber,
            passwordHash,
            registeredAt,
            isEmailConfirmed,
            confirmationCode
            );
    }
}
