namespace Erdmier.BlazorCore.Components.Exceptions;

internal sealed class MultipleCoreElseStatementsException : CoreInvalidOperationException
{
    public MultipleCoreElseStatementsException()
        : base(message: $"A {nameof(CoreIfControl)} component may only contain one {nameof(CoreElseStatement)} component.")
    { }
}
