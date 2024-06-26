namespace Erdmier.BlazorCore.Components.Exceptions;

internal sealed class CoreIfControlRequiresCoreIfStatementException : CoreInvalidOperationException
{
    public CoreIfControlRequiresCoreIfStatementException()
        : base(message: $"A {nameof(CoreIfControl)} component must contain a {nameof(CoreIfStatement)} component.")
    { }
}
