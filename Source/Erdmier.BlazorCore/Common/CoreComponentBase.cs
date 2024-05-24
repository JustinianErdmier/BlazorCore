// ---------------------------------------------------------------------------------------------------------------------------------
// Justin Erdmier licenses this file to you under the MIT license.
// Erdmier.BlazorCore > Erdmier.BlazorCore > CoreComponentBase.cs
// Modified: 12 11, 2022
// ---------------------------------------------------------------------------------------------------------------------------------

namespace Erdmier.BlazorCore.Common;

/// <summary>
///     Base class for components who correspond to an HTML element - provides common properties for customizing the
///     rendering of the HTML element.
/// </summary>
public abstract class CoreComponentBase : ComponentBase
{
    /// <summary> Sets the value of the CSS <c> class </c> attribute. </summary>
    [ Parameter ]
    public virtual string? Class { protected get; set; }
    
    /// <summary> Sets the keys and values for additional attributes that will be added to the element. </summary>
    [ Parameter ]
    public virtual Dictionary<string, object>? Attributes { protected get; set; }
}
