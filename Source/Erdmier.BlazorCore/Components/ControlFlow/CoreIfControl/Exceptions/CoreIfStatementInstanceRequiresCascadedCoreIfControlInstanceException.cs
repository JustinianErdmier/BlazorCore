namespace Erdmier.BlazorCore.Components.Exceptions;

internal sealed class CoreIfStatementInstanceRequiresCascadedCoreIfControlInstanceException : CoreArgumentNullException
{
    public CoreIfStatementInstanceRequiresCascadedCoreIfControlInstanceException()
        : base(parameterName: nameof(CoreIfStatement.Wrapper),
               message: $"A {nameof(CoreIfStatement)} component must be used within a {nameof(CoreIfControl)} component.")
    { }
}
