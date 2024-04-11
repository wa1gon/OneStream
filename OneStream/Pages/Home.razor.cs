namespace OneStream.Pages;

public partial class Home : ComponentBase
{
    [Inject] protected IMatToaster Toaster { get; set; }
    private string Callsign { get; set; } = string.Empty;
    private string Notes { get; set; } = string.Empty;
    public async Task OnClick(MouseEventArgs e)
    {
        Toaster.Add("Toaster test " + Callsign, MatToastType.Danger, "title");
    }
}
