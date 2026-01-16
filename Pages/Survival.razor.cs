using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace SnakeGame.Pages;

public partial class Survival : ComponentBase, IDisposable
{
    private int energia = 100;
    private DotNetObjectReference<Survival>? objRef;
    [Inject]
    private IJSRuntime JS { get; set; } = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            objRef = DotNetObjectReference.Create(this);
            await JS.InvokeVoidAsync("initThreeJS", "threejs-container", objRef);
        }
    }

    private async Task HandleKey(KeyboardEventArgs e)
    {
        if (e.Key == "w") await JS.InvokeVoidAsync("moveCamera", 0.5);
        if (e.Key == "s") await JS.InvokeVoidAsync("moveCamera", -0.5);
        if (e.Key == "a") await JS.InvokeVoidAsync("rotateCamera", 0.1);
        if (e.Key == "d") await JS.InvokeVoidAsync("rotateCamera", -0.1);
    }

    [JSInvokable]
    public void AdicionarPontoInstancia()
    {
        if (energia > 0)
        {
            energia -= 10;

            if (energia < 0) energia = 0;

            InvokeAsync(StateHasChanged);
        }
    }

    public void Dispose() => objRef?.Dispose();
}