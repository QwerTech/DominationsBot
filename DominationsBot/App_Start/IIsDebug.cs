namespace GCR
{
    public interface IIsDebug
    {
        bool IsDebugDatabase { get; }
        bool IsDebugCode { get; }
        bool IsAnyDebug { get; }
    }
}