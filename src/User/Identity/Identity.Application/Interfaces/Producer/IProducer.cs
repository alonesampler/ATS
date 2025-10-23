namespace Identity.Application.Interfaces.Producer;

public interface IProducer<TMessege> where TMessege : class
{
    Task ProduceAsync(TMessege messege, int? partition = null);
}
