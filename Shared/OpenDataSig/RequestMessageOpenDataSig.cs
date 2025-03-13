namespace Shared.OpenDataSig
{
    public class RequestMessageOpenDataSig
    {
        public string Message { get; set; }
        public string? ThreadId { get; set; }
        public List<double>? Coordinates { get; set; }
    }
}
