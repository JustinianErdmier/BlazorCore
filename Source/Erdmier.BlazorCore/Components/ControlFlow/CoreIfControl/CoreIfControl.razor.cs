namespace Erdmier.BlazorCore.Components;

/// <summary> A component easily allowing you to conditionally display markup without context switching between <c> C# </c> and <c> Razor </c>. </summary>
/// <remarks> This component, nor its children components, directly render any <c> HTML </c>. </remarks>
public partial class CoreIfControl : ComponentBase
{
    /// <summary> The <see cref="CoreElseIfStatement" /> component instances. </summary>
    private Dictionary<int, CoreElseIfStatement> _elseIfStatements = [];

    /// <summary> The <see cref="CoreElseStatement" /> instance, if registered. </summary>
    private CoreElseStatement? _elseStatement;

    /// <summary> The <see cref="CoreIfStatement" /> instance, if registered. </summary>
    private CoreIfStatement _ifStatement = null!;

    /// <summary> Gets the <see cref="ICoreIfControlStatement" /> to be rendered. </summary>
    /// <remarks> Is updated after <see cref="EvaluateBooleanExpressions" /> is invoked. </remarks>
    internal ICoreIfControlStatement? StatementToBeRendered { get; private set; }

    /// <summary> Gets or sets the markup to be rendered. </summary>
    [ Parameter ]
    public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc />
    protected override void OnAfterRender(bool firstRender)
    {
        if (_ifStatement is null)
        {
            throw new CoreIfControlRequiresCoreIfStatementException();
        }

        base.OnAfterRender(firstRender);
    }

    /// <summary> Evaluates the <c> Predicate </c> property for the registered statements. </summary>
    /// <remarks>
    ///     <para>
    ///         This method starts with evaluating the <see cref="_ifStatement" />. Then it loops through the <see cref="_elseIfStatements" /> in the order they were
    ///         added. At the first <c> Predicate </c> to be <c> TRUE </c>, it will set <see cref="StatementToBeRendered" /> and return.
    ///     </para>
    ///     <para>
    ///         If all <c> Predicate </c>s are <c> FALSE </c>, then <see cref="StatementToBeRendered" /> will be set to <see cref="_elseStatement" />, if one was
    ///         registered.
    ///     </para>
    /// </remarks>
    internal void EvaluateBooleanExpressions()
    {
        if (_ifStatement.BooleanExpressionResult)
        {
            StatementToBeRendered = _ifStatement;

            goto Update;
        }

        foreach ((int _, CoreElseIfStatement elseIfStatement) in _elseIfStatements.OrderBy(statement => statement.Key))
        {
            if (!elseIfStatement.BooleanExpressionResult)
            {
                continue;
            }

            StatementToBeRendered = elseIfStatement;

            goto Update;
        }

        StatementToBeRendered = _elseStatement;

        Update:

        StateHasChanged();
    }

    /// <summary> Registers the given <paramref name="controlStatement" /> with the <see cref="CoreIfControl" /> instance. </summary>
    /// <param name="controlStatement"> The <see cref="ICoreIfControlStatement" /> instance to be registered. </param>
    internal void RegisterControlStatement(ICoreIfControlStatement controlStatement)
    {
        switch (controlStatement)
        {
            case CoreIfStatement ifStatement:
                RegisterIfStatement(ifStatement);

                break;

            case CoreElseIfStatement elseIfStatement:
                RegisterElseIfStatement(elseIfStatement);

                break;

            case CoreElseStatement elseStatement:
                RegisterElseStatement(elseStatement);

                break;
        }
    }

    /// <summary> Registers the given <paramref name="ifStatement" /> with the <see cref="CoreIfControl" /> instance. </summary>
    /// <param name="ifStatement"> The <see cref="CoreIfStatement" /> instance to be registered. </param>
    /// <exception cref="MultipleCoreIfStatementsException"> Throws if the <see cref="CoreIfStatement" /> instance has already been registered. </exception>
    private void RegisterIfStatement(CoreIfStatement ifStatement)
    {
        if (_ifStatement is not null)
        {
            throw new MultipleCoreIfStatementsException();
        }

        _ifStatement = ifStatement;
    }

    /// <summary>Registers the given <paramref name="elseIfStatement" /> with the <see cref="CoreIfControl" /> instance.</summary>
    /// <param name="elseIfStatement"> The <see cref="CoreElseIfStatement" /> instance to be registered. </param>
    /// <exception cref="CoreElseIfStatementRequiresCoreIfStatementException"> Throws if the <see cref="CoreIfStatement" /> instance has not already been registered. </exception>
    /// <exception cref="MultipleCoreElseIfStatementsException">Throws if the <see cref="CoreElseIfStatement" /> instance has already been registered.</exception>
    private void RegisterElseIfStatement(CoreElseIfStatement elseIfStatement)
    {
        if (_ifStatement is null)
        {
            throw new CoreElseIfStatementRequiresCoreIfStatementException();
        }

        if (_elseIfStatements.ContainsValue(elseIfStatement))
        {
            throw new MultipleCoreElseIfStatementsException();
        }

        int lastIndex = _elseIfStatements.Keys.LastOrDefault();

        _elseIfStatements.Add(key: lastIndex + 1, elseIfStatement);
    }

    /// <summary>Registers the given <paramref name="elseStatement" /> with the <see cref="CoreIfControl" /> instance.</summary>
    /// <param name="elseStatement">The <see cref="CoreElseStatement" /> instance to be registered.</param>
    /// <exception cref="CoreElseStatementRequiresCoreIfStatementException">Throws if the <see cref="CoreIfStatement" /> instance has not already been registered.</exception>
    /// <exception cref="MultipleCoreElseStatementsException">Throws if the <see cref="CoreElseStatement" /> instance has already been registered.</exception>
    private void RegisterElseStatement(CoreElseStatement elseStatement)
    {
        if (_ifStatement is null)
        {
            throw new CoreElseStatementRequiresCoreIfStatementException();
        }

        if (_elseStatement is not null)
        {
            throw new MultipleCoreElseStatementsException();
        }

        _elseStatement = elseStatement;
    }
}
