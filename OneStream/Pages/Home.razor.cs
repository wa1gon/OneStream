namespace OneStream.Pages;

public partial class Home : ComponentBase
{
    [Inject] protected IMatToaster Toaster { get; set; }
    [Inject] protected ILookupService LookupService { get; set; }
    private string Callsign { get; set; } = string.Empty;
    private string Notes { get; set; } = string.Empty;
    public async Task LookupClick(MouseEventArgs e)
    {
        //Toaster.Add("Toaster test " + Callsign, MatToastType.Danger, "title");
        var callInfo = await LookupService.GetCallsignDetailsAsync(Callsign);
    }

    private async Task ReplaceNotesClick(MouseEventArgs arg)
    {

        throw new NotImplementedException();
    }

    private async Task AddNotesClick()
    {
        throw new NotImplementedException();
    }
}
