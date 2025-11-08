using FluentResults;
using Identity.Application.Interfaces.Producer;
using Identity.Application.Interfaces.UseCases;
using Identity.Domain.Events;
using Identity.Domain.Interfaces;

namespace Identity.Application.UseCases;

public class ConfirmEmailUseCase(
    IUnitOfWork unitOfWork,
    IProducer<EmailConfirmedEvent> producer) : IConfirmEmailUseCase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProducer<EmailConfirmedEvent> _producer = producer;

    public async Task<Result> ExecuteAsync(Guid userId, string code)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

        if (user is null)
            return Result.Fail(ErrorMessage.UserNotFound);

        await _unitOfWork.BeginTransactionAsync();
        user.ConfirmEmail(code);
        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.CommitTransactionAsync();

        var message = new EmailConfirmedEvent(
            user.Id,
            user.Email.Value);

        await _producer.ProduceAsync(message);

        return Result.Ok();
    }
}
