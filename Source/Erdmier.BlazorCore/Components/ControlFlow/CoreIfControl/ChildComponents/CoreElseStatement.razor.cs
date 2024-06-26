namespace Erdmier.BlazorCore.Components;

public partial class CoreElseStatement : ComponentBase, ICoreIfControlStatement
{
    /// <summary> Gets or sets the Razor markup to be rendered. </summary>
    [ Parameter ]
    public RenderFragment? ChildContent { get; set; }

    [ CascadingParameter ]
    public CoreIfControl Controller { get; set; } = null!;

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        if (Controller is null)
        {
            throw new CoreElseStatementInstanceRequiresCascadedCoreIfControlInstanceException();
        }

        Controller.RegisterControlStatement(controlStatement: this);

        base.OnInitialized();
    }
}
