namespace Identity.Infrastructure.Brokers;

public class TopicConfig<TMessage> where TMessage : class
{
    public string TopicName { get; set; }
    
    public int Particion { get; set; }
}
