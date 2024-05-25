namespace Erdmier.BlazorCore.Components.Exceptions;

internal sealed class MultipleCoreElseIfStatementInstancesException : CoreInvalidOperationException
{
    public MultipleCoreElseIfStatementInstancesException()
        : base(message: $"A {nameof(CoreIfControl)} component may not contain multiple instances of a {nameof(CoreElseIfStatement)} component.")
    { }
}
