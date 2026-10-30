using Part3_BuilderPattern.Model;

namespace Part3_BuilderPattern.Builders;

public class SimpleInvoiceBuilder
{
    private int? _invoiceId;
    private string? _customerName;
    private string? _customerEmail;
    private string? _customerPhone;

    private string? _billingStreet;
    private string? _billingCity;
    private string? _billingState;
    private string? _billingZipCode;
    private string? _billingCountry;

    private string? _shippingStreet;
    private string? _shippingCity;
    private string? _shippingState;
    private string? _shippingZipCode;
    private string? _shippingCountry;

    private DateTime? _orderDate;
    private string? _paymentMethod;
    private string? _currency;
    private decimal? _subTotal;
    private decimal _discountAmount = 0m;
    private decimal _taxAmount = 0m;
    private decimal? _totalAmount;

    public SimpleInvoiceBuilder WithInvoiceId(int invoiceId)
    {
        _invoiceId = invoiceId;
        return this;
    }

    public SimpleInvoiceBuilder WithCustomer(string name, string email, string phone)
    {
        _customerName = name;
        _customerEmail = email;
        _customerPhone = phone;
        return this;
    }

    public SimpleInvoiceBuilder WithBillingAddress(string street, string city, string state, string zipCode, string country)
    {
        _billingStreet = street;
        _billingCity = city;
        _billingState = state;
        _billingZipCode = zipCode;
        _billingCountry = country;
        return this;
    }

    public SimpleInvoiceBuilder WithShippingAddress(string street, string city, string state, string zipCode, string country)
    {
        _shippingStreet = street;
        _shippingCity = city;
        _shippingState = state;
        _shippingZipCode = zipCode;
        _shippingCountry = country;
        return this;
    }

    public SimpleInvoiceBuilder WithOrder(DateTime orderDate, string paymentMethod, string currency, decimal subTotal, decimal discount = 0m, decimal tax = 0m)
    {
        _orderDate = orderDate;
        _paymentMethod = paymentMethod;
        _currency = currency;
        _subTotal = subTotal;
        _discountAmount = discount;
        _taxAmount = tax;
        _totalAmount = subTotal - discount + tax;
        return this;
    }

    public Invoice Build()
    {
        // Enforce mandatory fields
        if (!_invoiceId.HasValue)
            throw new InvalidOperationException("InvoiceId is mandatory.");

        if (string.IsNullOrWhiteSpace(_customerName))
            throw new InvalidOperationException("CustomerName is mandatory.");

        if (string.IsNullOrWhiteSpace(_billingStreet) || string.IsNullOrWhiteSpace(_billingCity) || string.IsNullOrWhiteSpace(_billingCountry))
            throw new InvalidOperationException("Billing Address (street, city, country) is mandatory.");

        if (string.IsNullOrWhiteSpace(_shippingStreet) || string.IsNullOrWhiteSpace(_shippingCity) || string.IsNullOrWhiteSpace(_shippingCountry))
            throw new InvalidOperationException("Shipping Address (street, city, country) is mandatory.");

        if (!_subTotal.HasValue || string.IsNullOrWhiteSpace(_currency))
            throw new InvalidOperationException("Order SubTotal and Currency are mandatory.");

        var billing = new Address(
            _billingStreet,
            _billingCity,
            _billingState ?? string.Empty,
            _billingZipCode ?? string.Empty,
            _billingCountry
        );

        var shipping = new Address(
            _shippingStreet,
            _shippingCity,
            _shippingState ?? string.Empty,
            _shippingZipCode ?? string.Empty,
            _shippingCountry
        );

        var order = new OrderDetails(
            _orderDate ?? DateTime.UtcNow,
            _paymentMethod ?? "Unspecified",
            _currency,
            _subTotal.Value,
            _discountAmount,
            _taxAmount,
            _totalAmount ?? (_subTotal.Value - _discountAmount + _taxAmount)
        );

        return new Invoice(
            _invoiceId.Value,
            _customerName,
            _customerEmail ?? string.Empty,
            _customerPhone ?? string.Empty,
            billing,
            shipping,
            order
        );
    }
}
