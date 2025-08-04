namespace HelloDependencyInjection.Services
{
    public class ApplicationSettings
    {
        public string DefaultPaymentMethod { get; }

        public ApplicationSettings()
        {
            DefaultPaymentMethod = $"[{GetHashCode()}]\tCreditCard";
        }
    }
}
