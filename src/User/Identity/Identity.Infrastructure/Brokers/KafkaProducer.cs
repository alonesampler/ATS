using Identity.Application.Interfaces.Producer;
using MassTransit;

namespace Identity.Infrastructure.Brokers;

public class KafkaProducer<TMessage> : IProducer<TMessage>
    where TMessage : class
{
    private readonly ITopicProducer<TMessage> _producer;

    public KafkaProducer(ITopicProducer<TMessage> producer)
    {
        _producer = producer;
    }

    public async Task ProduceAsync(TMessage message, int? partition = null)
    {
        if (partition.HasValue)
            await _producer.Produce(message, Pipe.Execute<KafkaSendContext>(x => x.Partition = partition.Value));
        else
            await _producer.Produce(message);
    }
}
