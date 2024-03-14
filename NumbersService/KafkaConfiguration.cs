namespace NumbersService;

public class KafkaConfiguration
{
    public string BootstrapServersOut { get; set; }
    public string BootstrapServersIn { get; set; }
    public string Topic { get; set; }
    public string GroupId { get; set; }
}
