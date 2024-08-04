using OneTheater.Modules.Users.Domain.Abstractions;

namespace OneTheater.Modules.Users.Application.Abstractions.Exceptions;
public sealed class OneTheaterException : Exception
{
    public OneTheaterException(string requestName, Error? error = default, Exception? innerException = default) : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get;}
    public Error? Error { get; }
}
