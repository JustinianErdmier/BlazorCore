namespace Erdmier.BlazorCore.Components.Exceptions;

internal sealed class CoreElseStatementRequiresCoreIfStatementException : CoreInvalidOperationException
{
    public CoreElseStatementRequiresCoreIfStatementException()
        : base(message:
               $"A {nameof(CoreIfControl)} component may not contain a {nameof(CoreElseStatement)} component without containing a {nameof(CoreIfStatement)} component.")
    { }
}
