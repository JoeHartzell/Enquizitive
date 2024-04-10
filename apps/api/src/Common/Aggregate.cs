namespace Enquizitive.Common;

public abstract class Aggregate<TEvent> where TEvent : IDomainEvent
{
    private readonly List<TEvent> _events = [];

    /// <summary>
    /// A unique identifier for the aggregate.
    /// </summary>
    public Guid Id { get; protected set; } = Guid.NewGuid();

    /// <summary>
    /// The type of the aggregate.
    /// </summary>
    public abstract string Type { get; }

    /// <summary>
    /// List of domain events that have occurred on the aggregate.
    /// </summary>
    public IReadOnlyList<TEvent> Events => _events.AsReadOnly();

    /// <summary>
    /// Clears the list of domain events.
    /// </summary>
    public void ClearEvents()
    {
        _events.Clear();
    }

    protected void RaiseEvent(TEvent @event)
    {
        _events.Add(@event);
        ApplyEvent(@event);
    }

    protected abstract void ApplyEvent(TEvent @event);
}