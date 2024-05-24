// ---------------------------------------------------------------------------------------------------------------------------------
// Justin Erdmier licenses this file to you under the MIT license.
// Erdmier.BlazorCore > Erdmier.BlazorCore > ExampleJsInterop.cs
// Modified: 12 11, 2022
// ---------------------------------------------------------------------------------------------------------------------------------

namespace Erdmier.BlazorCore;

// This class provides an example of how JavaScript functionality can be wrapped
// in a .NET class for easy consumption. The associated JavaScript module is
// loaded on demand when first needed.
//
// This class can be registered as scoped DI service and then injected into Blazor
// components for use.

public class ExampleJsInterop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;
    
    public ExampleJsInterop(IJSRuntime jsRuntime)
    {
        moduleTask = new Lazy<Task<IJSObjectReference>>(() => jsRuntime.InvokeAsync<IJSObjectReference>(identifier: "import", "./_content/Erdmier.BlazorCore/exampleJsInterop.js")
                                                                       .AsTask());
    }
    
    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            IJSObjectReference module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
    
    public async ValueTask<string> Prompt(string message)
    {
        IJSObjectReference module = await moduleTask.Value;
        
        return await module.InvokeAsync<string>(identifier: "showPrompt", message);
    }
}
