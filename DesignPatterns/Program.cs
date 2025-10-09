// ============================================================================
// 🏭 FACTORY METHOD DESIGN PATTERN — PROFESSIONAL REFERENCE IMPLEMENTATION
// ============================================================================
//
// AUTHOR:  Usama Mukhyer
// PURPOSE:  Demonstrates the complete Factory Method pattern in C#, including
//           abstract factories, concrete factories, product hierarchies,
//           centralized factory provider, and explanatory comments.
//
// ----------------------------------------------------------------------------
// 🔹 Pattern Category : Creational
// 🔹 Core Intent      : Define an interface for object creation, but let
//                      subclasses decide which concrete class to instantiate.
//
// ----------------------------------------------------------------------------
// WHY IT EXISTS
// ----------------------------------------------------------------------------
// • In object-oriented design, "new" is a code smell when repeated everywhere.
// • If creation logic is scattered across the codebase, the system becomes
//   tightly coupled to specific implementations.
// • The Factory Method pattern encapsulates object creation in a dedicated
//   hierarchy so that client code depends only on abstractions, not concretes.
//
// ----------------------------------------------------------------------------
// GOALS
// ----------------------------------------------------------------------------
// ✅ Remove direct dependency on concrete classes.
// ✅ Centralize and standardize object creation logic.
// ✅ Reuse common logic (template behaviour) in a base factory.
// ✅ Follow SOLID principles: especially Open–Closed & Dependency Inversion.
// ✅ Enable easy runtime substitution of implementations.
//
// ============================================================================

using System;

namespace FactoryPatternExample
{
    // ------------------------------------------------------------------------
    // 1️⃣  PRODUCT INTERFACE  →  Defines the abstraction all products share.
    // ------------------------------------------------------------------------
    public interface INotification
    {
        void NotifyUser(string message);
    }

    // ------------------------------------------------------------------------
    // 2️⃣  CONCRETE PRODUCTS  →  Real implementations of the abstraction.
    // ------------------------------------------------------------------------
    public class EmailNotification : INotification
    {
        public void NotifyUser(string message)
            => Console.WriteLine($"📧 Email Notification → {message}");
    }

    public class SMSNotification : INotification
    {
        public void NotifyUser(string message)
            => Console.WriteLine($"📱 SMS Notification → {message}");
    }

    public class WhatsAppNotification : INotification
    {
        public void NotifyUser(string message)
            => Console.WriteLine($"💬 WhatsApp Notification → {message}");
    }

    // ------------------------------------------------------------------------
    // 3️⃣  ABSTRACT FACTORY  →  Declares the Factory Method and optional
    //                           reusable business logic.
    // ------------------------------------------------------------------------
    public abstract class NotificationFactory
    {
        // Factory Method — must be implemented by subclasses.
        public abstract INotification CreateNotification();

        // Common reusable logic shared by all factories.
        public void Send(string message)
        {
            // The base class calls the Factory Method to obtain a product.
            INotification notification = CreateNotification();

            // All subclasses reuse this workflow; only creation differs.
            notification.NotifyUser(message);
        }
    }

    // ------------------------------------------------------------------------
    // 4️⃣  CONCRETE FACTORIES  →  Real "factories" that decide what to create.
    // ------------------------------------------------------------------------
    public class EmailNotificationFactory : NotificationFactory
    {
        public override INotification CreateNotification()
            => new EmailNotification();
    }

    public class SMSNotificationFactory : NotificationFactory
    {
        public override INotification CreateNotification()
            => new SMSNotification();
    }

    public class WhatsAppNotificationFactory : NotificationFactory
    {
        public override INotification CreateNotification()
            => new WhatsAppNotification();
    }

    // ------------------------------------------------------------------------
    // 5️⃣  FACTORY PROVIDER (META-FACTORY)
    //      Centralizes selection of the appropriate concrete factory.
    //      In enterprise systems this layer is often replaced by
    //      Dependency Injection or configuration-based mapping.
    // ------------------------------------------------------------------------
    public static class FactoryProvider
    {
        public static NotificationFactory GetFactory(string type)
        {
            return type switch
            {
                "email" => new EmailNotificationFactory(),
                "sms" => new SMSNotificationFactory(),
                "whatsapp" => new WhatsAppNotificationFactory(),
                _ => throw new NotSupportedException(
                        $"Notification type '{type}' is not supported.")
            };
        }
    }

    // ------------------------------------------------------------------------
    // 6️⃣  CLIENT (MAIN)  →  Uses factories, never products directly.
    // ------------------------------------------------------------------------
    public class Program
    {
        public static void Main()
        {
            Console.Title = "Factory Method Pattern Demo";
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("=== Factory Method Pattern Demo ===\n");
            Console.Write("Enter notification type (email / sms / whatsapp): ");

            string? type = Console.ReadLine()?.Trim().ToLower();

            try
            {
                // Client delegates creation to Factory Provider.
                NotificationFactory factory = FactoryProvider.GetFactory(type!);

                // Client calls the shared business method.
                factory.Send("Message sent using Factory Method Pattern!");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n✅ Operation successful.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n❌ Error: {ex.Message}");
            }
            finally
            {
                Console.ResetColor();
                Console.WriteLine("\nPress any key to exit ...");
                Console.ReadKey();
            }
        }
    }
}

// ============================================================================
// 🧭  PROFESSIONAL SUMMARY
// ============================================================================
//
// FACTORY METHOD PATTERN — KEY ROLES
// ----------------------------------
// 1. Product Interface (INotification)
//    • Defines the common contract all products must fulfill.
//
// 2. Concrete Products (EmailNotification, SMSNotification, WhatsAppNotification)
//    • Provide their own implementations of that contract.
//
// 3. Abstract Creator / Factory (NotificationFactory)
//    • Declares the abstract Factory Method (CreateNotification).
//    • May contain reusable business logic (Send).
//
// 4. Concrete Creators / Factories
//    • Implement the Factory Method to produce concrete products.
//
// 5. Client (Program)
//    • Works solely with the abstract interfaces.
//    • Chooses an appropriate factory (possibly via configuration or DI).
//
// ----------------------------------------------------------------------------
// HOW IT DIFFERS FROM SIMPLE POLYMORPHISM
// ---------------------------------------
// • Polymorphism changes *behavior* of existing objects.
// • Factory Method changes *which object* is created in the first place.
// • They often work together — creation (Factory) + behavior (Polymorphism).
//
// ----------------------------------------------------------------------------
// EVOLUTION PATH IN ENTERPRISE SYSTEMS
// ------------------------------------
//   Small app        → if/else in Main (simple Factory Method)
//   Medium app       → FactoryProvider (centralized creation)
//   Large app        → Abstract Factory + Dependency Injection (container)
//
// ----------------------------------------------------------------------------
// ASCII UML OVERVIEW
// ------------------------------------
//            ┌──────────────────────────┐
//            │  NotificationFactory     │  (abstract creator)
//            │  + CreateNotification() │◄──────────────┐
//            │  + Send()               │               │
//            └───────────────▲─────────┘               │
//                            │                         │
//        ┌───────────────────┴──────────────────┐       │
//        │ EmailFactory   SMSFactory   WhatsAppFactory │
//        └────────▲──────────▲──────────▲──────────────┘
//                 │          │          │
//          ┌──────┴──────┐ ┌─┴────────┐ ┌────────────┐
//          │ EmailNotif. │ │ SMSNotif │ │ WhatsAppN. │
//          └─────────────┘ └──────────┘ └────────────┘
//                 ▲
//                 │
//           ┌─────┴─────┐
//           │   INotification  │
//           └──────────────────┘
//
// ============================================================================
// ☕  ANALOGY — Coffee Machine
// ============================================================================
// • Abstract Factory → Blueprint for Coffee Machine (createCoffee())
// • Concrete Factory → Specific Machine (LatteMachine, EspressoMachine)
// • Product Interface → ICoffee
// • Concrete Product → Latte, Espresso
// • Client → Customer pressing a button
//
// ============================================================================
// 🔸  GOLDEN RULE
// ============================================================================
//     "You will always add something new,
//      but with the Factory Method pattern,
//      you add it once —in the factory—
//      instead of everywhere in the codebase."
// ============================================================================
