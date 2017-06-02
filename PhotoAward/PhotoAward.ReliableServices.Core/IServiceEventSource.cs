using System.Diagnostics.Tracing;

namespace PhotoAward.ReliableServices.Core
{
    public interface IServiceEventSource
    {
        void Message(string message, params object[] args);
        void Message(string message);
        bool IsEnabled();
        bool IsEnabled(EventLevel level, EventKeywords keywords);
        void Dispose();
    }
}