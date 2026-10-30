using Part3_BuilderPattern.Builders;

namespace Part3_BuilderPattern;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== BUILDER PATTERN DEMONSTRATION ===\n");

        // 1. Task 3.2: Simple Fluent Builder
        Console.WriteLine("--- 1. Constructing with Simple Fluent Builder (Task 3.2) ---");
        var simpleInvoice = new SimpleInvoiceBuilder()
            .WithInvoiceId(1001)
            .WithCustomer("Ahmed Eissa", "ahmed@example.com", "+201011122233")
            .WithBillingAddress("15 Tahrir St", "Cairo", "Cairo", "11511", "Egypt")
            .WithShippingAddress("45 Corniche Rd", "Alexandria", "Alexandria", "21500", "Egypt")
            .WithOrder(DateTime.UtcNow, "Credit Card", "USD", 1200m, discount: 100m, tax: 154m)
            .Build();

        PrintInvoiceSummary("Simple Builder Invoice", simpleInvoice);

        // 2. Task 3.3: Composed Hierarchical Builders
        Console.WriteLine("\n--- 2. Constructing with Composed Builders (Task 3.3) ---");
        var composedInvoice = new ComposedInvoiceBuilder()
            .WithInvoiceId(2002)
            .WithCustomer("Sara Mansour", "sara@example.com", "+201099887766")
            .WithBillingAddress(b => b
                .WithStreet("10 Al-Azhar St")
                .WithCity("Cairo")
                .WithState("Cairo")
                .WithZipCode("11633")
                .WithCountry("Egypt"))
            .WithShippingAddress(s => s
                .WithStreet("22 Port Said St")
                .WithCity("Ismailia")
                .WithState("Ismailia")
                .WithZipCode("41511")
                .WithCountry("Egypt"))
            .WithOrderDetails(o => o
                .WithOrderDate(DateTime.UtcNow)
                .WithPaymentMethod("Bank Transfer")
                .WithCurrency("EGP")
                .WithAmounts(subTotal: 5000m, discount: 250m, tax: 665m))
            .Build();

        PrintInvoiceSummary("Composed Builder Invoice", composedInvoice);

        // 3. Mandatory Fields Validation Test
        Console.WriteLine("\n--- 3. Validation: Omission of Mandatory Field ---");
        try
        {
            new ComposedInvoiceBuilder()
                .WithInvoiceId(3003)
                // Missing customer and addresses
                .Build();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"[PASS] Validation caught missing required field: {ex.Message}");
        }
    }

    private static void PrintInvoiceSummary(string title, Model.Invoice invoice)
    {
        Console.WriteLine($"[{title}]");
        Console.WriteLine($"Invoice #{invoice.InvoiceId} for {invoice.CustomerName} ({invoice.CustomerEmail})");
        Console.WriteLine($"Billing Address: {invoice.BillingAddress}");
        Console.WriteLine($"Shipping Address: {invoice.ShippingAddress}");
        Console.WriteLine($"Payment: {invoice.PaymentMethod} | SubTotal: {invoice.SubTotal} {invoice.Currency} | Total: {invoice.TotalAmount} {invoice.Currency}");
    }
}
