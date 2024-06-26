namespace Erdmier.BlazorCore.Components;

public partial class CoreIfStatement : ComponentBase, ICoreIfControlStatement
{
    private bool _previousBooleanExpressionResult;

    /// <summary> Gets or sets the Razor markup to be conditionally rendered based on the <see cref="BooleanExpressionResult" />. </summary>
    [ Parameter ]
    public RenderFragment? ChildContent { get; set; }

    /// <summary> Gets or sets the result of a boolean expression used to conditionally render the given <see cref="ChildContent" />. </summary>
    [ Parameter ]
    public bool BooleanExpressionResult { get; set; }

    [ CascadingParameter ]
    public CoreIfControl Controller { get; set; } = null!;

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        if (Controller is null)
        {
            throw new CoreIfStatementInstanceRequiresCascadedCoreIfControlInstanceException();
        }

        Controller.RegisterControlStatement(controlStatement: this);

        base.OnInitialized();
    }

    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        if (BooleanExpressionResult != _previousBooleanExpressionResult)
        {
            _previousBooleanExpressionResult = BooleanExpressionResult;

            Controller.EvaluateBooleanExpressions();
        }

        base.OnParametersSetAsync();
    }
}
