namespace Erdmier.BlazorCore.Common.Exceptions;

internal class CoreInvalidOperationException : InvalidOperationException
{
    public CoreInvalidOperationException(string? message = null)
        : base(message)
    { }
}
