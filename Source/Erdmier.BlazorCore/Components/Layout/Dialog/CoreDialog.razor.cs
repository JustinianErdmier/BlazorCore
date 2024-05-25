// ---------------------------------------------------------------------------------------------------------------------------------
// Justin Erdmier licenses this file to you under the MIT license.
// Erdmier.BlazorCore > Erdmier.BlazorCore > CoreDialog.razor.cs
// Modified: 24 11, 2022
// ---------------------------------------------------------------------------------------------------------------------------------

namespace Erdmier.BlazorCore.Components;

/// <summary> A component taking advantage of the built-in <c> dialog </c> element. </summary>
/// <remarks>
///     <para>
///         <b> Note about subscribing to the <c> close </c> event: </b> <br /> If you are needing to subscribe to the <c> close </c> event, bind to the
///         <see cref="OnClose" /> parameter. Do not use the <see cref="OnCloseEvent" />; this is for JSInterop only.
///     </para>
/// </remarks>
public partial class CoreDialog : CoreComponentBase
{
    /// <summary> Represents the reference to a DOM's dialog element which is bound to an <see cref="CoreDialog" /> instance. </summary>
    private ElementReference _element;

    /// <summary> Represents the reference to a <see cref="CoreDialog" /> instance which is bound to a DOM's dialog element. </summary>
    private DotNetObjectReference<CoreDialog> _this = null!;

    /// <summary> Gets or sets whether the dialog is shown and can be interacted with by the user. </summary>
    [ Parameter ]
    public bool IsShown { get; set; }

    /// <summary> Enables two-way binding for <see cref="IsShown" />. </summary>
    [ Parameter ]
    public EventCallback<bool> IsShownChanged { get; set; }

    /// <summary> Gets or sets the value which determines whether the dialog is a modal dialog. </summary>
    /// <remarks>
    ///     <para>
    ///         This value ultimately determines which <c> show() </c> function is called on the DOM's <c> dialog </c> element. If this value is <c> TRUE </c>, then
    ///         <c> showModal() </c> is called. Otherwise, <c> show() </c>.
    ///     </para>
    ///     <para>
    ///         While there are some more nuanced differences between the two, the main difference is that if this value is <c> TRUE </c>, then users will be able
    ///         to close the dialog using the <c> ESC </c> key.
    ///     </para>
    ///     <para> To learn more about the <c> dialog </c> element and the differences between setting it as a modal, check the two <i> see also </i> links. </para>
    /// </remarks>
    /// <seealso href="https://developer.mozilla.org/en-US/docs/web/html/element/dialog" />
    /// <seealso href="https://html.spec.whatwg.org/multipage/interactive-elements.html#the-dialog-element" />
    [ Parameter ]
    public bool IsModal { get; set; }

    /// <summary> Gets or sets an <see cref="EventCallback{TValue}" /> which returns a <see cref="string" /> when the DOM's dialog element is closed. </summary>
    [ Parameter ]
    public EventCallback<string?> OnClose { get; set; }

    /// <summary> Gets or sets the Razor markup to be rendered within the <see cref="CoreDialog" />. </summary>
    [ Parameter ]
    public RenderFragment? ChildContent { get; set; }

    /// <summary> Gets the component's <see cref="CoreComponentBase.Attributes" /> but filters out the <c> tabindex </c> and <c> open </c> attributes. </summary>
    /// <remarks> The <c> dialog </c> element must not contain the <c> tabindex </c> attribute nor should have the <c> open </c> attribute manually set. </remarks>
    private Dictionary<string, object>? DialogAttributes =>
        Attributes?.Where(attr => !string.Equals(attr.Key, b: "tabindex") && !string.Equals(attr.Key, b: "open"))
                  .ToDictionary(attr => attr.Key, attr => attr.Value);

    /// <inheritdoc />
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // If this is the first render, then bind this .NET object's reference with the reference to the corresponding DOM's dialog.
            _this = DotNetObjectReference.Create(this);
            await JsRuntime.InvokeVoidAsync(identifier: "initializeDialog", _element, _this);
        }

        await ToggleState();

        await base.OnAfterRenderAsync(firstRender);
    }

    /// <summary> This method is called by the <see cref="IJSRuntime" /> when the <c> dialog </c> element closes. </summary>
    /// <param name="returnValue"> The value returned by the <c> dialog </c> element upon closing. </param>
    /// <remarks>
    ///     <para>
    ///         <c> dialog </c> elements can be closed by <c> form </c> elements if their <c> method </c> attribute is set to <c> method="dialog" </c>. When a
    ///         <c> dialog </c> element is closed in this way, the <c> returnValue </c> is set to the value of the <c> value </c> attribute of the <c> button </c>
    ///         element used to submit the <c> form </c> element.
    ///     </para>
    ///     <para>
    ///         <b> Note: </b> If you are consuming this <see cref="CoreDialog" />, do not try to subscribe/bind to this method. Use the <see cref="OnClose" />
    ///         property.
    ///     </para>
    /// </remarks>
    [ JSInvokable ]
    public async Task OnCloseEvent(string? returnValue)
    {
        // If the dialog is shown, then close it and invoke the OnClose event callback.
        if (IsShown)
        {
            IsShown = false;
            await IsShownChanged.InvokeAsync(IsShown);
        }

        await OnClose.InvokeAsync(returnValue);
    }

    /// <summary>
    ///     Uses the <see cref="IJSRuntime" /> to show or close the <c> dialog </c> element. Determines whether to use the <c> show() </c> or
    ///     <c> showModal() </c> method using the value <see cref="IsModal" />.
    /// </summary>
    private async Task ToggleState()
    {
        switch (IsShown)
        {
            case true when IsModal:
                await JsRuntime.InvokeVoidAsync(identifier: "showDialogModal", _element);

                break;

            case true:
                await JsRuntime.InvokeVoidAsync(identifier: "showDialog", _element);

                break;

            case false:
                await JsRuntime.InvokeVoidAsync(identifier: "closeDialog", _element);

                break;
        }
    }
}
