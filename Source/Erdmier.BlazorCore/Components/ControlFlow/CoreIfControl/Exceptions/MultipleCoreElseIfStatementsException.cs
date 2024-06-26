namespace Erdmier.BlazorCore.Components.Exceptions;

internal sealed class MultipleCoreElseIfStatementsException : CoreInvalidOperationException
{
    public MultipleCoreElseIfStatementsException()
        : base(message: $"A {nameof(CoreIfControl)} component may not contain multiple instances of a {nameof(CoreElseIfStatement)} component.")
    { }
}
