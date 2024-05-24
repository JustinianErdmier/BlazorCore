// ---------------------------------------------------------------------------------------------------------------------------------
// Justin Erdmier licenses this file to you under the MIT license.
// Erdmier.BlazorCore > Erdmier.BlazorCore > CoreDialogWrapper.cs
// Modified: 24 11, 2022
// ---------------------------------------------------------------------------------------------------------------------------------

namespace Erdmier.BlazorCore.Layout.Dialog;

/// <summary>
///     Provides a wrapper for common properties and methods used when consuming a <see cref="CoreDialog" />
///     component.
/// </summary>
/// <remarks>
///     Just inherit your dialog wrapping component from this and use the available properties and methods. If you
///     need more control over the close behavior, you can override the <see cref="CloseDialog" /> and
///     <see cref="OnDialogClosed" /> methods.
/// </remarks>
public abstract class CoreDialogWrapper : ComponentBase
{
    /// <summary> Gets or sets the value determining whether the <see cref="CoreDialog" /> is shown. </summary>
    /// <remarks>
    ///     <b> Note: </b> This property is meant to be two-way bound with the <see cref="CoreDialog.IsShown" /> property.
    ///     <example>
    ///         <CoreDialog IsShown="@IsDialogShown"> @* your content here... *@ </CoreDialog>
    ///     </example>
    /// </remarks>
    [ Parameter ]
    public bool IsDialogShown { get; set; }
    
    /// <summary> Enables two-way binding for the <see cref="IsDialogShown" /> property. </summary>
    [ Parameter ]
    public EventCallback<bool> IsDialogShownChanged { get; set; }
    
    /// <summary> Gets or sets the <see cref="EventCallback{TValue}" /> of type nullable string to be invoked when the dialog is closed. </summary>
    [ Parameter ]
    public EventCallback<string?> DialogClosed { get; set; }
    
    /// <summary> Default implementation for closing the <see cref="CoreDialog" />. </summary>
    /// <remarks> If you need more control over the close behavior, you can override this method. </remarks>
    protected virtual async Task CloseDialog()
    {
        IsDialogShown = false;
        await IsDialogShownChanged.InvokeAsync(IsDialogShown);
    }
    
    /// <summary> Default implementation for the <see cref="DialogClosed" /> event. </summary>
    /// <param name="returnValue"> The value, if any, returned by the <c> dialog </c> element upon being closed. </param>
    /// <remarks>
    ///     Subscribe the <see cref="CoreDialog.OnClose" /> event to this method to get notified when the <c> dialog </c>
    ///     is closed. If you need more control over the close event behavior, you can override this method.
    /// </remarks>
    protected virtual async Task OnDialogClosed(string? returnValue) => await DialogClosed.InvokeAsync(returnValue);
}
