using HelloDependencyInjection.Contracts;
using HelloDependencyInjection.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HelloDependencyInjection;

internal class Program
{
    static void Main(string[] args)
    {
        // DIP: Dependency Inversion Principle
        // Abhaegigkeiten werden hier erzeugt
        var settings = new ApplicationSettings();
        var paymentService = new PaymentService(settings);
        var shoppingCartService = new ShoppingCartService(paymentService);
        
        shoppingCartService.AddToCart("Laptop");
        shoppingCartService.Checkout();

        Console.WriteLine("\n\nDependency Injection mit Microsoft.Extensions.DependencyInjection");

        var provider = RegisterDependencyTypesOnStartUpOnce();

        // Beim Aufloesen des Service wird der "Dependency Tree" automatisch aufgeloest, d. h.
        // IPaymentService und ApplicationSettings werden erzeugt
        var shoppingCartService1 = provider.GetService<IShoppingCartService>();
        shoppingCartService1.AddToCart("Karotte");
        shoppingCartService1.Checkout();


        Console.WriteLine("\n\nVerwendung von Scopes");
        using (var scope = provider.CreateScope())
        {
            var scopedShoppingCart = scope.ServiceProvider.GetService<IShoppingCartService>();
            scopedShoppingCart.AddToCart("Brot");
            scopedShoppingCart.Checkout();

            var scopedShoppingCart2 = scope.ServiceProvider.GetService<IShoppingCartService>();
            scopedShoppingCart2.AddToCart("Eier");
            scopedShoppingCart2.Checkout();
        }

        Console.WriteLine("\n\nWeiteren Scope erzeugen");
        using (var scope = provider.CreateScope())
        {
            var scopedShoppingCart = scope.ServiceProvider.GetService<IShoppingCartService>();
            scopedShoppingCart.AddToCart("Wein");
            scopedShoppingCart.Checkout();
        }


        Console.ReadKey();
    }

    private static ServiceProvider RegisterDependencyTypesOnStartUpOnce()
    {
        var collection = new ServiceCollection();

        // Transient: Neue Instanzen werden bei jedem Aufruf erzeugt, also jedes mal wird "new" verwendet
        collection.AddTransient<IShoppingCartService, ShoppingCartService>(); // Shopping Cart Service>

        // Scoped: Neue Instanzen werden bei jedem Aufruf erzeugt, aber nur einmal pro Scope
        collection.AddScoped<IPaymentService, PaymentService>(); // Payment Service

        // Singleton: Neue Instanzen werden nur einmal erzeugt fuer den gesamten Lebenszyklus der Anwendung
        collection.AddSingleton<ApplicationSettings>(); // Application Settings

        var serviceProvider = collection.BuildServiceProvider();
        return serviceProvider;
    }
}
