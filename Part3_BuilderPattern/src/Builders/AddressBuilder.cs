using Part3_BuilderPattern.Model;

namespace Part3_BuilderPattern.Builders;

public class AddressBuilder
{
    private string? _street;
    private string? _city;
    private string? _state;
    private string? _zipCode;
    private string? _country;

    public AddressBuilder WithStreet(string street)
    {
        _street = street;
        return this;
    }

    public AddressBuilder WithCity(string city)
    {
        _city = city;
        return this;
    }

    public AddressBuilder WithState(string state)
    {
        _state = state;
        return this;
    }

    public AddressBuilder WithZipCode(string zipCode)
    {
        _zipCode = zipCode;
        return this;
    }

    public AddressBuilder WithCountry(string country)
    {
        _country = country;
        return this;
    }

    public Address Build()
    {
        if (string.IsNullOrWhiteSpace(_street))
            throw new InvalidOperationException("Street is mandatory for an address.");

        if (string.IsNullOrWhiteSpace(_city))
            throw new InvalidOperationException("City is mandatory for an address.");

        if (string.IsNullOrWhiteSpace(_country))
            throw new InvalidOperationException("Country is mandatory for an address.");

        return new Address(
            _street,
            _city,
            _state ?? string.Empty,
            _zipCode ?? string.Empty,
            _country
        );
    }
}
