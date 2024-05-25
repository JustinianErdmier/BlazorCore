namespace Erdmier.BlazorCore.Components.Exceptions;

internal sealed class CoreElseIfStatementInstanceRequiresCascadedCoreIfControlInstanceException : CoreArgumentNullException
{
    public CoreElseIfStatementInstanceRequiresCascadedCoreIfControlInstanceException()
        : base(parameterName: nameof(CoreElseIfStatement.Wrapper),
               message: $"A {nameof(CoreElseIfStatement)} component must be used within a {nameof(CoreIfControl)} component.")
    { }
}
