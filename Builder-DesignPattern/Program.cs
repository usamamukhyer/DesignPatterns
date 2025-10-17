// ============================================================================
// 🧱 BUILDER DESIGN PATTERN — FULL PROFESSIONAL CONSOLE EXAMPLE
// ============================================================================
//
// AUTHOR:  Usama Mukhyer
// PURPOSE: Demonstrates the Builder pattern in C# with full inline explanation.
//
// ----------------------------------------------------------------------------
// GOAL:
//   • Construct complex objects step-by-step (e.g., Computer, Vehicle)
//   • Separate object construction (Builder) from its representation (Product)
//   • Allow same construction process to create different representations.
//
// ----------------------------------------------------------------------------
// BENEFITS:
//   ✅ Simplifies object creation with many optional parameters
//   ✅ Allows chaining (fluent API)
//   ✅ Ensures immutability of final product
//   ✅ Makes code more readable & maintainable
// ============================================================================

using System;

namespace BuilderPatternDemo
{
    // ------------------------------------------------------------------------
    // 1️⃣ PRODUCT CLASS — The complex object being built.
    // ------------------------------------------------------------------------
    public class Computer
    {
        public string CPU { get; set; } = "";
        public string GPU { get; set; } = "";
        public string RAM { get; set; } = "";
        public string Storage { get; set; } = "";
        public string PowerSupply { get; set; } = "";

        public void Display()
        {
            Console.WriteLine("💻 Computer Configuration:");
            Console.WriteLine($"   CPU: {CPU}");
            Console.WriteLine($"   GPU: {GPU}");
            Console.WriteLine($"   RAM: {RAM}");
            Console.WriteLine($"   Storage: {Storage}");
            Console.WriteLine($"   Power Supply: {PowerSupply}");
        }
    }

    // ------------------------------------------------------------------------
    // 2️⃣ BUILDER INTERFACE — Specifies steps for building the product.
    // ------------------------------------------------------------------------
    public interface IComputerBuilder
    {
        void SetCPU();
        void SetGPU();
        void SetRAM();
        void SetStorage();
        void SetPowerSupply();
        Computer GetComputer();
    }

    // ------------------------------------------------------------------------
    // 3️⃣ CONCRETE BUILDERS — Implement step-by-step building for each variant.
    // ------------------------------------------------------------------------

    // 🧠 "Gaming PC" Builder
    public class GamingComputerBuilder : IComputerBuilder
    {
        private Computer _computer = new Computer();

        public void SetCPU() => _computer.CPU = "Intel Core i9 13900K";
        public void SetGPU() => _computer.GPU = "NVIDIA RTX 4090";
        public void SetRAM() => _computer.RAM = "32GB DDR5";
        public void SetStorage() => _computer.Storage = "2TB NVMe SSD";
        public void SetPowerSupply() => _computer.PowerSupply = "1000W Gold PSU";

        public Computer GetComputer() => _computer;
    }

    // 💼 "Office PC" Builder
    public class OfficeComputerBuilder : IComputerBuilder
    {
        private Computer _computer = new Computer();

        public void SetCPU() => _computer.CPU = "Intel Core i5 12400";
        public void SetGPU() => _computer.GPU = "Integrated Intel UHD Graphics";
        public void SetRAM() => _computer.RAM = "16GB DDR4";
        public void SetStorage() => _computer.Storage = "512GB SSD";
        public void SetPowerSupply() => _computer.PowerSupply = "500W Bronze PSU";

        public Computer GetComputer() => _computer;
    }

    // ------------------------------------------------------------------------
    // 4️⃣ DIRECTOR CLASS — Defines the building sequence.
    // ------------------------------------------------------------------------
    public class ComputerDirector
    {
        private readonly IComputerBuilder _builder;

        // 💡 Composition — Director "has-a" builder (not inherits from it)
        public ComputerDirector(IComputerBuilder builder)
        {
            _builder = builder;
        }

        public void ConstructComputer()
        {
            Console.WriteLine("🛠️ Starting computer construction...");
            _builder.SetCPU();
            _builder.SetGPU();
            _builder.SetRAM();
            _builder.SetStorage();
            _builder.SetPowerSupply();
            Console.WriteLine("✅ Computer successfully built!\n");
        }

        public Computer GetComputer() => _builder.GetComputer();
    }

    // ------------------------------------------------------------------------
    // 5️⃣ CLIENT (Main) — Chooses which builder to use at runtime.
    // ------------------------------------------------------------------------
    public class Program
    {
        public static void Main()
        {
            Console.Title = "Builder Design Pattern Demo";
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("=== 🧱 Builder Design Pattern Demo ===\n");
            Console.Write("Enter computer type (gaming / office): ");

            string? type = Console.ReadLine()?.Trim().ToLower();
            IComputerBuilder builder;

            if (type == "gaming")
            {
                builder = new GamingComputerBuilder();
                Console.WriteLine("\n➡️ Selected Gaming Computer Builder");
            }
            else if (type == "office")
            {
                builder = new OfficeComputerBuilder();
                Console.WriteLine("\n➡️ Selected Office Computer Builder");
            }
            else
            {
                throw new Exception("❌ Unknown computer type!");
            }

            // 👇 Composition — Director HAS a builder
            ComputerDirector director = new ComputerDirector(builder);

            // 🔨 Build product using the same construction process
            director.ConstructComputer();

            // 🧱 Get final product
            Computer computer = director.GetComputer();
            computer.Display();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n🎉 Demo complete — object built step-by-step using Builder Pattern.\n");
            Console.ResetColor();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
