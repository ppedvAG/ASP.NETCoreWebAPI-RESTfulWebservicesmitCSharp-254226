namespace HelloDependencyInjection.Contracts
{
    public interface IShoppingCartService
    {
        void AddToCart(string product);
        void Checkout();
    }
}