using Part3_BuilderPattern.Model;

namespace Part3_BuilderPattern.Builders;

public class ComposedInvoiceBuilder
{
    private int? _invoiceId;
    private string? _customerName;
    private string? _customerEmail;
    private string? _customerPhone;

    private Address? _billingAddress;
    private Address? _shippingAddress;
    private OrderDetails? _orderDetails;

    public ComposedInvoiceBuilder WithInvoiceId(int invoiceId)
    {
        _invoiceId = invoiceId;
        return this;
    }

    public ComposedInvoiceBuilder WithCustomer(string name, string email, string phone)
    {
        _customerName = name;
        _customerEmail = email;
        _customerPhone = phone;
        return this;
    }

    public ComposedInvoiceBuilder WithBillingAddress(Action<AddressBuilder> configure)
    {
        var builder = new AddressBuilder();
        configure(builder);
        _billingAddress = builder.Build();
        return this;
    }

    public ComposedInvoiceBuilder WithBillingAddress(Address address)
    {
        _billingAddress = address;
        return this;
    }

    public ComposedInvoiceBuilder WithShippingAddress(Action<AddressBuilder> configure)
    {
        var builder = new AddressBuilder();
        configure(builder);
        _shippingAddress = builder.Build();
        return this;
    }

    public ComposedInvoiceBuilder WithShippingAddress(Address address)
    {
        _shippingAddress = address;
        return this;
    }

    public ComposedInvoiceBuilder WithOrderDetails(Action<OrderBuilder> configure)
    {
        var builder = new OrderBuilder();
        configure(builder);
        _orderDetails = builder.Build();
        return this;
    }

    public ComposedInvoiceBuilder WithOrderDetails(OrderDetails orderDetails)
    {
        _orderDetails = orderDetails;
        return this;
    }

    public Invoice Build()
    {
        if (!_invoiceId.HasValue)
            throw new InvalidOperationException("InvoiceId is mandatory.");

        if (string.IsNullOrWhiteSpace(_customerName))
            throw new InvalidOperationException("CustomerName is mandatory.");

        if (_billingAddress == null)
            throw new InvalidOperationException("Billing address is mandatory.");

        if (_shippingAddress == null)
            throw new InvalidOperationException("Shipping address is mandatory.");

        if (_orderDetails == null)
            throw new InvalidOperationException("Order details are mandatory.");

        return new Invoice(
            _invoiceId.Value,
            _customerName,
            _customerEmail ?? string.Empty,
            _customerPhone ?? string.Empty,
            _billingAddress,
            _shippingAddress,
            _orderDetails
        );
    }
}
