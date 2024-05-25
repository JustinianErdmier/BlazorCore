namespace Erdmier.BlazorCore.Components;

public partial class CoreIfStatement : ComponentBase, ICoreIfControlStatement
{
    private bool _predicate;

    /// <summary> Gets or sets the Razor markup to be conditionally rendered based on the <see cref="Predicate" />. </summary>
    [ Parameter ]
    public RenderFragment? ChildContent { get; set; }

    /// <summary> Gets or sets the predicate used to conditionally render the given <see cref="ChildContent" />. </summary>
    [ Parameter ]
    public bool Predicate { get; set; }

    [ CascadingParameter ]
    public CoreIfControl Wrapper { get; set; } = null!;

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        if (Wrapper is null)
        {
            throw new CoreIfStatementInstanceRequiresCascadedCoreIfControlInstanceException();
        }

        Wrapper.RegisterControlStatement(controlStatement: this);

        base.OnInitialized();
    }

    /// <inheritdoc />
    protected override async Task OnParametersSetAsync()
    {
        if (Predicate != _predicate)
        {
            _predicate = Predicate;

            Wrapper.EvaluatePredicates();
        }

        await base.OnParametersSetAsync();
    }
}
