namespace OpenAQMVC.Models
{
    public class ErrorViewModel
    {
        public string? ErrorMessage { get; init; }

        public string? RequestId { get; init; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}