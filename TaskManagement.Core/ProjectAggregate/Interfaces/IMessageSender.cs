namespace TaskManagement.Core.ProjectAggregate.Interfaces
{
    public interface IMessageSender
    {
        TaskItem SendMessagesAsync();
    }
}
