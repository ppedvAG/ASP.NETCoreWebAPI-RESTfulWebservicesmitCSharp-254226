using HelloDependencyInjection.Contracts;

namespace HelloDependencyInjection.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IPaymentService _paymentService;

        // DIP (Dependency Inversion Principle) besagt, dass wir Abhängigkeiten
        // von außen uebergeben sollten.
        //PaymentService _paymentService = new PaymentService();

        public ShoppingCartService(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public void AddToCart(string product)
        {
            Console.WriteLine($"[{GetHashCode()}]\t{product} hinzugefügt.");
        }

        public void Checkout()
        {
            _paymentService.MakePayment(100);
        }
    }
}
