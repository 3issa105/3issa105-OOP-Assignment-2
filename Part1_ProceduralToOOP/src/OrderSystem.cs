namespace Part1_ProceduralToOOP;

public class OrderSystem
{
    private readonly List<Customer> _customers = new();
    private readonly List<Product> _products = new();
    private readonly List<Order> _orders = new();

    public IReadOnlyList<Customer> Customers => _customers;
    public IReadOnlyList<Product> Products => _products;
    public IReadOnlyList<Order> Orders => _orders;

    public Customer? FindCustomerById(int id)
    {
        return _customers.FirstOrDefault(c => c.Id == id);
    }

    public Product? FindProductById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public Order? FindOrderById(int id)
    {
        return _orders.FirstOrDefault(o => o.Id == id);
    }

    public bool AddCustomer(int id, string name, string email, string city, bool isVip)
    {
        if (FindCustomerById(id) != null)
        {
            Console.WriteLine($"ERROR: customer id {id} already exists.");
            return false;
        }

        _customers.Add(new Customer(id, name, email, city, isVip));
        return true;
    }

    public void PrintCustomers()
    {
        Console.WriteLine($"\n=== CUSTOMERS ({_customers.Count}) ===");
        foreach (var c in _customers)
        {
            string vipStatus = c.IsVip ? "yes" : "no";
            Console.WriteLine($"#{c.Id}  {c.Name}  <{c.Email}>  {c.City}  vip={vipStatus}");
        }
    }

    public bool AddProduct(int id, string name, decimal price, int stock)
    {
        if (FindProductById(id) != null)
        {
            Console.WriteLine($"ERROR: product id {id} already exists.");
            return false;
        }

        _products.Add(new Product(id, name, price, stock));
        return true;
    }

    public void PrintProducts()
    {
        Console.WriteLine($"\n=== PRODUCTS ({_products.Count}) ===");
        foreach (var p in _products)
        {
            Console.WriteLine($"#{p.Id}  {p.Name}  price={p.Price:F2}  stock={p.Stock}");
        }
    }

    public bool CreateOrder(int orderId, int customerId, string date)
    {
        if (FindOrderById(orderId) != null)
        {
            Console.WriteLine($"ERROR: order id {orderId} already exists.");
            return false;
        }

        var customer = FindCustomerById(customerId);
        if (customer == null)
        {
            Console.WriteLine($"ERROR: customer id {customerId} not found.");
            return false;
        }

        _orders.Add(new Order(orderId, customer, date));
        return true;
    }

    public bool AddLineToOrder(int orderId, int productId, int quantity)
    {
        var order = FindOrderById(orderId);
        if (order == null)
        {
            Console.WriteLine($"ERROR: order id {orderId} not found.");
            return false;
        }

        var product = FindProductById(productId);
        if (product == null)
        {
            Console.WriteLine($"ERROR: product id {productId} not found.");
            return false;
        }

        if (!order.TryAddLine(product, quantity, out string error))
        {
            Console.WriteLine($"ERROR: {error}");
            return false;
        }

        return true;
    }

    public bool MarkOrderPaid(int orderId)
    {
        var order = FindOrderById(orderId);
        if (order == null)
        {
            Console.WriteLine($"ERROR: order id {orderId} not found.");
            return false;
        }

        if (!order.TryMarkPaid(out string error))
        {
            Console.WriteLine($"ERROR: {error}");
            return false;
        }

        return true;
    }

    public void PrintOrder(int orderId)
    {
        var order = FindOrderById(orderId);
        if (order == null)
        {
            Console.WriteLine($"ERROR: order id {orderId} not found.");
            return;
        }

        Console.WriteLine($"\n=== ORDER #{order.Id} ===");
        Console.WriteLine($"Date: {order.Date}");
        Console.WriteLine($"Customer: {order.Customer.Name} (#{order.Customer.Id})");
        Console.WriteLine($"Paid: {(order.IsPaid ? "yes" : "no")}");
        Console.WriteLine("Lines:");

        foreach (var line in order.Lines)
        {
            Console.WriteLine($"  - {line.Product.Name}  x{line.Quantity}  @{line.Product.Price:F2}  = {line.LineTotal:F2}");
        }

        Console.WriteLine($"TOTAL: {order.CalculateTotal():F2}");
    }

    public void PrintAllOrders()
    {
        Console.WriteLine($"\n=== ALL ORDERS ({_orders.Count}) ===");
        foreach (var order in _orders)
        {
            PrintOrder(order.Id);
        }
    }

    public decimal TotalSalesPaidOnly()
    {
        return _orders.Where(o => o.IsPaid).Sum(o => o.CalculateTotal());
    }

    public void SeedSampleData()
    {
        AddCustomer(1, "Mona Ali", "mona@example.com", "Cairo", true);
        AddCustomer(2, "Omar Hassan", "omar@example.com", "Alexandria", false);
        AddCustomer(3, "Sara Nabil", "sara@example.com", "Giza", false);

        AddProduct(101, "USB Cable", 50.0m, 100);
        AddProduct(102, "Wireless Mouse", 250.0m, 40);
        AddProduct(103, "Mechanical Keyboard", 1200.0m, 15);
        AddProduct(104, "Laptop Stand", 400.0m, 25);
    }

    public void RunDemoScenario()
    {
        CreateOrder(1001, 1, "2026-09-15");
        AddLineToOrder(1001, 101, 2);
        AddLineToOrder(1001, 102, 1);
        MarkOrderPaid(1001);

        CreateOrder(1002, 2, "2026-09-15");
        AddLineToOrder(1002, 103, 1);
        AddLineToOrder(1002, 104, 1);

        CreateOrder(1003, 3, "2026-09-16");
        AddLineToOrder(1003, 101, 5);
        MarkOrderPaid(1003);
    }
}
