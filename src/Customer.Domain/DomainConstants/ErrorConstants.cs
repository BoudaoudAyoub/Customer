namespace CustomerMan.Domain.CustomerConstants;
public static class ErrorConstants
{
    public const string CUSTOMER_NAME_MUST_NOT_BE_EMPTY = "the name must not be empty";
    public const string CUSTOMER_ALREADY_EXISTS = "Customer {0} already exists";
    public const string UNSUPPORTED_IMAGE_EXTENTION = "Please make sure that you are using the correct image extentions. The supported extentions are: .jpeg, .jpg, .png";
    public const string INVALID_CONTACT_EMAIL_FORMAT = "this email must be in the following format 'example@subdomain.TLD'";
    public const string CUSTOMER_DOESNT_EXISTS = "Customer doesn't exists";
    public const string CUSTOMER_ID_MUST_NOT_BE_EMPTY_GUID = "Customer Id must not be empty GUID";
    public const string TO_RESTORE_CUSTOMER = "To restore a customer, please ensure that the customer has been previously deleted";
}