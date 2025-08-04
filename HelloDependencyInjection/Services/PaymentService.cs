using HelloDependencyInjection.Contracts;

namespace HelloDependencyInjection.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationSettings _settings;

        public PaymentService(ApplicationSettings settings)
        {
            _settings = settings;
        }

        public void MakePayment(decimal amount)
        {
            // payment logic
            Console.WriteLine($"{_settings.DefaultPaymentMethod}");

            Console.WriteLine($"[{GetHashCode()}]\tZahlung von {amount} Euro durchgeführt.");
        }
    }
}
