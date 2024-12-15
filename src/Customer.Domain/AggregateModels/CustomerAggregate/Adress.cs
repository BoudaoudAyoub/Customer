using SharedKernel.Seedwork;
namespace CustomerMan.Domain.AggregateModels.CustomerAggregate;
public class Adress : ValueObject
{
    public string City { get; private set; } = default!;
    public string State { get; private set; } = default!;
    public string Street { get; private set; } = default!;
    public string Country { get; private set; } = default!;
    public string ZipCode { get; private set; } = default!;

    public Adress(){}

    public Adress(string street, string city, string state, string country, string zipCode)
    {
        City = city;
        State = state;
        Street = street;
        Country = country;
        ZipCode = zipCode;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        // Using a yield return statement to return each element one at a time
        yield return Street;
        yield return City;
        yield return State;
        yield return Country;
        yield return ZipCode;
    }
}
