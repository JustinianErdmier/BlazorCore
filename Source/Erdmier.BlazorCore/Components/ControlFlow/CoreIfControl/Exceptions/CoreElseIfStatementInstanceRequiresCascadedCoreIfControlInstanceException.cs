namespace Erdmier.BlazorCore.Components.Exceptions;

internal sealed class CoreElseIfStatementInstanceRequiresCascadedCoreIfControlInstanceException : CoreArgumentNullException
{
    public CoreElseIfStatementInstanceRequiresCascadedCoreIfControlInstanceException()
        : base(parameterName: nameof(CoreElseIfStatement.Controller),
               message: $"A {nameof(CoreElseIfStatement)} component must be used within a {nameof(CoreIfControl)} component.")
    { }
}
