namespace OpenAQWebApi.Results
{
    public class ErrorResult
    {
        public ErrorResult(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Cannot be null or whitespace.", nameof(message));

            ErrorMessage = message;
        }

        public string ErrorMessage { get; init; }
    }
}
