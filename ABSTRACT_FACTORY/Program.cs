// ============================================================================
// 🏭 ABSTRACT FACTORY DESIGN PATTERN — FULL PROFESSIONAL CONSOLE EXAMPLE
// ============================================================================
//
// AUTHOR:  Usama Mukhyer
// PURPOSE: Demonstrates composition-based Abstract Factory Pattern in C#
// ----------------------------------------------------------------------------
// GOAL:
//    • Build UI components (Button, Checkbox) for different platforms (Windows/Mac)
//    • Application should not care about concrete classes
//    • Easily extendable to more families (Linux, Web, Mobile)
//
// ----------------------------------------------------------------------------
// KEY DESIGN RULES:
//    ✅ Abstract Factory = Composition (Application HAS a Factory)
//    ✅ Factory Method   = Inheritance (Subclass IS a Factory)
// ----------------------------------------------------------------------------

using System;

namespace AbstractFactoryDemo
{
    // ------------------------------------------------------------------------
    // 1️⃣ ABSTRACT PRODUCT INTERFACES
    //    Each interface represents a product type in the family.
    // ------------------------------------------------------------------------
    public interface IButton
    {
        void Render();
    }

    public interface ICheckbox
    {
        void Render();
    }

    // ------------------------------------------------------------------------
    // 2️⃣ CONCRETE PRODUCTS
    //    These belong to specific "families" (Windows or Mac)
    // ------------------------------------------------------------------------

    // 🪟 WINDOWS FAMILY
    public class WindowsButton : IButton
    {
        public void Render() => Console.WriteLine("🪟 [WindowsButton] Rendered a Windows-style button");
    }

    public class WindowsCheckbox : ICheckbox
    {
        public void Render() => Console.WriteLine("🪟 [WindowsCheckbox] Rendered a Windows-style checkbox");
    }

    // 🍎 MAC FAMILY
    public class MacButton : IButton
    {
        public void Render() => Console.WriteLine("🍎 [MacButton] Rendered a Mac-style button");
    }

    public class MacCheckbox : ICheckbox
    {
        public void Render() => Console.WriteLine("🍎 [MacCheckbox] Rendered a Mac-style checkbox");
    }

    // ------------------------------------------------------------------------
    // 3️⃣ ABSTRACT FACTORY INTERFACE
    //    Declares creation methods for each product type.
    // ------------------------------------------------------------------------
    public interface IGUIFactory
    {
        IButton CreateButton();
        ICheckbox CreateCheckbox();
    }

    // ------------------------------------------------------------------------
    // 4️⃣ CONCRETE FACTORIES
    //    Implement abstract factory to produce products of one family.
    // ------------------------------------------------------------------------
    public class WindowsFactory : IGUIFactory
    {
        public IButton CreateButton() => new WindowsButton();
        public ICheckbox CreateCheckbox() => new WindowsCheckbox();
    }

    public class MacFactory : IGUIFactory
    {
        public IButton CreateButton() => new MacButton();
        public ICheckbox CreateCheckbox() => new MacCheckbox();
    }

    // ------------------------------------------------------------------------
    // 5️⃣ CLIENT CLASS (Application)
    //    Uses composition: HAS-A factory object, not inherits from it.
    // ------------------------------------------------------------------------
    public class Application
    {
        private readonly IButton _button;
        private readonly ICheckbox _checkbox;

        // 👇 Composition occurs here — the factory is passed (injected)
        public Application(IGUIFactory factory)
        {
            // Use the factory to create family of related objects
            _button = factory.CreateButton();
            _checkbox = factory.CreateCheckbox();
        }

        public void RenderUI()
        {
            Console.WriteLine("\n🧭 [Application] Rendering cross-platform UI...");
            _button.Render();
            _checkbox.Render();
            Console.WriteLine("✅ [Application] UI rendered successfully!\n");
        }
    }

    // ------------------------------------------------------------------------
    // 6️⃣ CLIENT ENTRY POINT (Program)
    //    Selects which factory to inject at runtime — COMPOSITION IN ACTION.
    // ------------------------------------------------------------------------
    public class Program
    {
        public static void Main()
        {
            Console.Title = "Abstract Factory Pattern Demo";
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("=== 🏭 Abstract Factory Pattern Demo ===\n");
            Console.Write("Enter platform (windows / mac): ");

            string? platform = Console.ReadLine()?.Trim().ToLower();

            // The concrete factory selected at runtime
            IGUIFactory factory;

            if (platform == "windows")
            {
                factory = new WindowsFactory();
                Console.WriteLine("\n➡️ Selected Windows UI Factory");
            }
            else if (platform == "mac")
            {
                factory = new MacFactory();
                Console.WriteLine("\n➡️ Selected Mac UI Factory");
            }
            else
            {
                throw new Exception("❌ Unknown platform. Please enter 'windows' or 'mac'.");
            }

            // 👇 Composition: Inject the chosen factory into the Application
            Application app = new Application(factory);

            // Render platform-specific UI without the app knowing concrete classes
            app.RenderUI();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("🎉 Demo complete — all components created through Abstract Factory.\n");
            Console.ResetColor();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
