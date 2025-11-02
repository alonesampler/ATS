using FluentResults;
using Identity.Application.Dtos.Request;
using Identity.Application.Interfaces.Producer;
using Identity.Application.Interfaces.UseCases;
using Identity.Domain.Entities;
using Identity.Domain.Events;
using Identity.Domain.Interfaces;
using Identity.Domain.ValueObjects;

namespace Identity.Application.UseCases;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProducer<UserRegisteredEvent> _producer;

    public RegisterUserUseCase(
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork,
        IProducer<UserRegisteredEvent> producer)
    {
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _producer = producer;
    }

    public async Task<Result> ExecuteAsync(UserDto createUserDto)
    {
        var user = CreateUserFromDto(createUserDto);

        await _unitOfWork.BeginTransactionAsync();
        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.CommitTransactionAsync();

        var message = new UserRegisteredEvent(
            user.Id,
            user.Email.Value,
            user.FullName.FirstName,
            user.FullName.LastName,
            user.EmailConfirmationCode
            );

        await _producer.ProduceAsync(message);

        return Result.Ok();
    }

    private User CreateUserFromDto(UserDto createUserDto)
    {
        var userId = Guid.NewGuid();

        var email = new Email(createUserDto.Email);

        var phoneNumber = createUserDto.PhoneNumber != null
            ? new PhoneNumber(createUserDto.PhoneNumber.CountryCode, createUserDto.PhoneNumber.Number)
            : null;

        var fullName = new FullName(
            createUserDto.FullName.FirstName,
            createUserDto.FullName.LastName,
            createUserDto.FullName.MiddleName
            );

        var hashedPassword = _passwordHasher.HashPassword(createUserDto.Password);
        var passwordHash = new PasswordHash(hashedPassword);

        var registeredAt = DateTime.UtcNow;
        var isEmailConfirmed = false;
        var confirmationCode = VereficationCode.Create();

        return User.Register(
            userId,
            email,
            phoneNumber,
            fullName,
            passwordHash,
            registeredAt,
            isEmailConfirmed,
            confirmationCode
            );
    }
}
