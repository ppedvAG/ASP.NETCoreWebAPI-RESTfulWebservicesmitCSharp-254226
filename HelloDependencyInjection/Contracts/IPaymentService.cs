namespace HelloDependencyInjection.Contracts
{
    public interface IPaymentService
    {
        void MakePayment(decimal amount);
    }
}