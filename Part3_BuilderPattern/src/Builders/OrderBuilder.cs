using Part3_BuilderPattern.Model;

namespace Part3_BuilderPattern.Builders;

public class OrderBuilder
{
    private DateTime? _orderDate;
    private string? _paymentMethod;
    private string? _currency;
    private decimal? _subTotal;
    private decimal _discountAmount = 0m;
    private decimal _taxAmount = 0m;

    public OrderBuilder WithOrderDate(DateTime orderDate)
    {
        _orderDate = orderDate;
        return this;
    }

    public OrderBuilder WithPaymentMethod(string paymentMethod)
    {
        _paymentMethod = paymentMethod;
        return this;
    }

    public OrderBuilder WithCurrency(string currency)
    {
        _currency = currency;
        return this;
    }

    public OrderBuilder WithAmounts(decimal subTotal, decimal discount = 0m, decimal tax = 0m)
    {
        if (subTotal < 0)
            throw new ArgumentException("SubTotal cannot be negative.");

        _subTotal = subTotal;
        _discountAmount = discount;
        _taxAmount = tax;
        return this;
    }

    public OrderDetails Build()
    {
        if (!_subTotal.HasValue)
            throw new InvalidOperationException("SubTotal is mandatory for order details.");

        if (string.IsNullOrWhiteSpace(_currency))
            throw new InvalidOperationException("Currency is mandatory for order details.");

        decimal total = _subTotal.Value - _discountAmount + _taxAmount;

        return new OrderDetails(
            _orderDate ?? DateTime.UtcNow,
            _paymentMethod ?? "Standard",
            _currency,
            _subTotal.Value,
            _discountAmount,
            _taxAmount,
            total
        );
    }
}
