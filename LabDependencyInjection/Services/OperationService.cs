namespace Lab002_DependencyInjection.Services
{
    public class OperationService : IOperationScoped, IOperationSingleton, IOperationTransient
    {
        // [^4..] - Kurze Schreibweise, um die letzten 4 Zeichen von der Guid zu erhalten
        public string OperationId { get; } = Guid.NewGuid().ToString()[^4..];

    }
}
