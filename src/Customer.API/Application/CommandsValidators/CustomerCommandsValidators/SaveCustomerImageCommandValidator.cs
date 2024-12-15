using CustomerMan.API.Application.Commands.CustomerCommands;
using CustomerMan.API.Application.CommandsValidations.CustomerCommandsValidations;
namespace CustomerMan.API.Application.CommandsValidators.CustomerCommandsValidators;
public class SaveCustomerImageCommandValidator : BaseCustomerCommandValidator<SaveCustomerImageCommand, string>
{
    public SaveCustomerImageCommandValidator()
    {
        ImageFileValidation();
    }
}
