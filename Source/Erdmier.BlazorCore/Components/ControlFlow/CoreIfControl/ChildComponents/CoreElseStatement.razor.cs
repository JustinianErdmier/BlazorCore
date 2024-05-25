namespace Erdmier.BlazorCore.Components;

public partial class CoreElseStatement : ComponentBase, ICoreIfControlStatement
{
    /// <summary> Gets or sets the Razor markup to be rendered. </summary>
    [ Parameter ]
    public RenderFragment? ChildContent { get; set; }

    [ CascadingParameter ]
    public CoreIfControl Wrapper { get; set; } = null!;

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        if (Wrapper is null)
        {
            throw new CoreElseStatementInstanceRequiresCascadedCoreIfControlInstanceException();
        }

        Wrapper.RegisterControlStatement(controlStatement: this);

        base.OnInitialized();
    }
}
