public interface ICoffee
{
    void Serve();
}

public class Espresso : ICoffee
{
    public void Serve() => Console.WriteLine("☕ Serving a strong Espresso");
}

public class Latte : ICoffee
{
    public void Serve() => Console.WriteLine("🥛 Serving a smooth Latte");
}

public abstract class CoffeeFactory
{
    public abstract ICoffee CreateCoffee();

    public void DeliverCoffee()
    {
        ICoffee coffee = CreateCoffee(); // factory method
        coffee.Serve();
    }
}

public class EspressoFactory : CoffeeFactory
{
    public override ICoffee CreateCoffee() => new Espresso();
}

public class LatteFactory : CoffeeFactory
{
    public override ICoffee CreateCoffee() => new Latte();
}

public class Program
{
    public static void Main()
    {
        CoffeeFactory factory;

        Console.Write("Enter coffee type (espresso/latte): ");
        string type = Console.ReadLine()?.Trim().ToLower();

        if (type == "espresso")
            factory = new EspressoFactory();
        else
            factory = new LatteFactory();

        factory.DeliverCoffee();
    }
}
