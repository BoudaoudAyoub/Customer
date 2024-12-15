using MediatR;
namespace CustomerMan.Domain.Seedwork;
public class Entity<T>
{
    private T _Id = default!;
    public virtual T ID
    {
        get => _Id; 
        set => _Id = value;
    }

    // Why Use INotification:
    // The use of INotification allows your domain model to dispatch events that are
    // decoupled from the handlers that consume these events. This fits well with
    // domain-driven design where domain events signal state changes within the domain

    private List<INotification> _domainEvents = default!;
    public IReadOnlyCollection<INotification>? DomainEvents => _domainEvents?.AsReadOnly();

    public void AddDomainEvent(INotification @event)
    {
        _domainEvents ??= [];
        _domainEvents.Add(@event);
    }

    public void RemoveDomainEvents(INotification @event) => _domainEvents?.Remove(@event);

    public void ClearDomainEvents() => _domainEvents?.Clear();
}