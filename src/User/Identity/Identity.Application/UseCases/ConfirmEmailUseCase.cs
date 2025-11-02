using FluentResults;
using Identity.Application.Interfaces.Producer;
using Identity.Application.Interfaces.UseCases;
using Identity.Domain.Events;
using Identity.Domain.Interfaces;

namespace Identity.Application.UseCases;

public class ConfirmEmailUseCase : IConfirmEmailUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProducer<EmailConfirmedEvent> _producer;

    public ConfirmEmailUseCase(
        IUnitOfWork unitOfWork,
        IProducer<EmailConfirmedEvent> producer)
    {
        _unitOfWork = unitOfWork;
        _producer = producer;
    }

    public async Task<Result> ExecuteAsync(Guid userId, string code)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

        if (user is null)
            return Result.Fail("User not found");

        await _unitOfWork.BeginTransactionAsync();
        user.ConfirmEmail(code);
        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.CommitTransactionAsync();

        var fullNumber = user.PhoneNumber != null
            ? $"{user.PhoneNumber.CountryCode}{user.PhoneNumber.Number}"
            : null;

        var message = new EmailConfirmedEvent(
            user.Id,
            user.Email.Value,
            user.FullName.FirstName,
            user.FullName.LastName,
            fullNumber);

        await _producer.ProduceAsync(message);

        return Result.Ok();
    }
}
