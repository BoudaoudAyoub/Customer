namespace CustomerMan.API.Application.Commands;
public class ApplicationCommand<Request, Response>(Guid commandId, Request command) where Request : IRequest<Response>
{
    public Guid CommandId { get; } = commandId;
    public Request Command { get; } = command;
}