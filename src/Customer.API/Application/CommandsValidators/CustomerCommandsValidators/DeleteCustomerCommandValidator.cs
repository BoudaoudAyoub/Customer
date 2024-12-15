using CustomerMan.API.Application.Commands.CustomerCommands;
using CustomerMan.API.Application.CommandsValidations.CustomerCommandsValidations;
namespace CustomerMan.API.Application.CommandsValidators.CustomerCommandsValidators;
public class DeleteCustomerCommandValidator : BaseCustomerCommandValidator<DeleteCustomerCommand, bool>
{
    public DeleteCustomerCommandValidator()
    {
        CustomerIdValidation();
    }
}