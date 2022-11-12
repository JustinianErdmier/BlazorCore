// ---------------------------------------------------------------------------------------------------------------------------------
// Justin Erdmier licenses this file to you under the MIT license.
// Erdmier.BlazorCore > Erdmier.BlazorCore.Demos > Error.cshtml.cs
// Modified: 12 11, 2022
// ---------------------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Erdmier.BlazorCore.Demos.Pages;

[ ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true), IgnoreAntiforgeryToken ]
public class ErrorModel : PageModel
{
    private readonly ILogger<ErrorModel> _logger;

    public ErrorModel(ILogger<ErrorModel> logger) => _logger = logger;

    public string? RequestId { get; set; }

    public bool ShowRequestId => ! string.IsNullOrEmpty(RequestId);

    public void OnGet()
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
}
