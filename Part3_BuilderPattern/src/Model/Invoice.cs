namespace Part3_BuilderPattern.Model;

public class Invoice
{
    // Customer Info
    public int InvoiceId { get; }
    public string CustomerName { get; }
    public string CustomerEmail { get; }
    public string CustomerPhone { get; }

    // Billing Address (Object and flat accessors)
    public Address BillingAddress { get; }
    public string BillingStreet => BillingAddress.Street;
    public string BillingCity => BillingAddress.City;
    public string BillingState => BillingAddress.State;
    public string BillingZipCode => BillingAddress.ZipCode;
    public string BillingCountry => BillingAddress.Country;

    // Shipping Address (Object and flat accessors)
    public Address ShippingAddress { get; }
    public string ShippingStreet => ShippingAddress.Street;
    public string ShippingCity => ShippingAddress.City;
    public string ShippingState => ShippingAddress.State;
    public string ShippingZipCode => ShippingAddress.ZipCode;
    public string ShippingCountry => ShippingAddress.Country;

    // Order & Payment Info (Object and flat accessors)
    public OrderDetails OrderInfo { get; }
    public DateTime OrderDate => OrderInfo.OrderDate;
    public string PaymentMethod => OrderInfo.PaymentMethod;
    public string Currency => OrderInfo.Currency;
    public decimal SubTotal => OrderInfo.SubTotal;
    public decimal DiscountAmount => OrderInfo.DiscountAmount;
    public decimal TaxAmount => OrderInfo.TaxAmount;
    public decimal TotalAmount => OrderInfo.TotalAmount;

    public Invoice(
        int invoiceId,
        string customerName,
        string customerEmail,
        string customerPhone,
        Address billingAddress,
        Address shippingAddress,
        OrderDetails orderInfo)
    {
        InvoiceId = invoiceId;
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        CustomerPhone = customerPhone;
        BillingAddress = billingAddress;
        ShippingAddress = shippingAddress;
        OrderInfo = orderInfo;
    }
}
