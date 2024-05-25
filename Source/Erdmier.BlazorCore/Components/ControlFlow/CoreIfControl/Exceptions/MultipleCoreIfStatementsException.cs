namespace Erdmier.BlazorCore.Components.Exceptions;

internal sealed class MultipleCoreIfStatementsException : CoreInvalidOperationException
{
    public MultipleCoreIfStatementsException()
        : base(message: $"A {nameof(CoreIfControl)} component may only contain one {nameof(CoreIfStatement)} component.")
    { }
}
