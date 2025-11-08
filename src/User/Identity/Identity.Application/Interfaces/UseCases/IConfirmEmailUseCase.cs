using FluentResults;

namespace Identity.Application.Interfaces.UseCases;

public interface IConfirmEmailUseCase
{
    Task<Result> ExecuteAsync(Guid userId, string code);
}