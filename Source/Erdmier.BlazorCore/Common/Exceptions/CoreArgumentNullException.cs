namespace Erdmier.BlazorCore.Common.Exceptions;

internal class CoreArgumentNullException : ArgumentNullException
{
    public CoreArgumentNullException(string parameterName, string? message = null)
        : base(parameterName, message)
    { }
}
