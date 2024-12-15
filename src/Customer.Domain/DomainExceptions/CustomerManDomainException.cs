using SharedKernel.Exceptions;
namespace CustomerMan.Domain.DomainExceptions;
public class CustomerManDomainException : CustomException
{
    // Make some logic for CustomerManDomainException

    public CustomerManDomainException(string message) : base(message) { }

    public CustomerManDomainException(string message, int statusCode) : base(message, statusCode) { }

    public CustomerManDomainException(IDictionary<int, string[]> errors) : base(errors) { }

    public CustomerManDomainException(string message, Exception innerException) 
        : base(message, innerException) { }
}