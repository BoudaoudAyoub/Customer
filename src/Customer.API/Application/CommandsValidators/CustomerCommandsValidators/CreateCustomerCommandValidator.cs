using CustomerMan.API.Application.Commands.CustomerCommands;
using CustomerMan.API.Application.CommandsValidations.CustomerCommandsValidations;
using CustomerMan.API.Application.Models.CustomerModels;
namespace CustomerMan.API.Application.CommandsValidators.CustomerCommandsValidators;
public class CreateCustomerCommandValidator : BaseCustomerCommandValidator<CreateCustomerCommand, CustomerModel>
{
    public CreateCustomerCommandValidator()
    {
        CustomerNameValidation();
        ContactEmailValidation();
    }
}