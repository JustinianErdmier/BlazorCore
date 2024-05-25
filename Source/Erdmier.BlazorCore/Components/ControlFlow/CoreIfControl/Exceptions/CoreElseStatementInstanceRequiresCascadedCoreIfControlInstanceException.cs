namespace Erdmier.BlazorCore.Components.Exceptions;

internal sealed class CoreElseStatementInstanceRequiresCascadedCoreIfControlInstanceException : CoreArgumentNullException
{
    public CoreElseStatementInstanceRequiresCascadedCoreIfControlInstanceException()
        : base(parameterName: nameof(CoreElseStatement.Wrapper),
               message: $"A {nameof(CoreElseStatement)} component must be used within a {nameof(CoreIfControl)} component.")
    { }
}
