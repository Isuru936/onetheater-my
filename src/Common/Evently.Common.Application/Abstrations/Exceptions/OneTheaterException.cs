using OneTheater.Common.Domain.Abstractions;

namespace OneTheater.Common.Application.Abstrations.Exceptions;
public sealed class OneTheaterException : Exception
{
    public OneTheaterException(string requestName, Error? error = default, Exception? innerException = default) : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }
    public Error? Error { get; }
}
