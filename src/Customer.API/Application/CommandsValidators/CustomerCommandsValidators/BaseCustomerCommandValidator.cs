using FluentValidation;
using CustomerMan.Domain.CustomerConstants;
using SharedKernel.Extentions.SupportedExtentions;
using CustomerMan.API.Application.Commands.CustomerCommands.Commands;
namespace CustomerMan.API.Application.CommandsValidations.CustomerCommandsValidations;
public class BaseCustomerCommandValidator<Command, Response> : AbstractValidator<Command> where Command : CustomerCommand<Response>
{
    protected void CustomerIdValidation() => RuleFor(c => c.Id).NotEqual(Guid.Empty).WithMessage(ErrorConstants.CUSTOMER_ID_MUST_NOT_BE_EMPTY_GUID);
   
    protected void CustomerNameValidation() => RuleFor(c => c.Name).NotEmpty().WithMessage(ErrorConstants.CUSTOMER_NAME_MUST_NOT_BE_EMPTY);

    protected void ContactEmailValidation() => RuleFor(c => c.ContactEmail).Matches(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")
                                                                           .When(w => !string.IsNullOrEmpty(w.ContactEmail))
                                                                           .WithMessage(ErrorConstants.INVALID_CONTACT_EMAIL_FORMAT);

    protected void ImageFileValidation()
    {
        RuleFor(c => c).Custom((command, context) =>
        {
            string[] extentions = 
            [
                ImageExtentions.JPGE,
                ImageExtentions.PNG,
                ImageExtentions.JPG
            ];

            string fileExtention = Path.GetExtension(command.Image.FileName).ToLower();

            if (!extentions.Contains(fileExtention))
            {
                context.AddFailure(ErrorConstants.UNSUPPORTED_IMAGE_EXTENTION);
            }

        })
        .When(c => c.Image is not null);
    }
}