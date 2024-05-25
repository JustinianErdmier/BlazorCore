namespace Erdmier.BlazorCore.Components.Exceptions;

internal sealed class CoreElseIfStatementRequiresCoreIfStatementException : CoreInvalidOperationException
{
    public CoreElseIfStatementRequiresCoreIfStatementException()
        : base(message:
               $"A {nameof(CoreIfControl)} component may not contain any {nameof(CoreElseIfStatement)} components without containing a {nameof(CoreIfStatement)} component.")
    { }
}
