namespace OneStream.Pages;

public partial class Home : ComponentBase
{
    [Inject] protected IMatToaster Toaster { get; set; }
    [Inject] protected ILookupService LookupService { get; set; }
    private string Callsign { get; set; } = string.Empty;
    private string Notes { get; set; } = string.Empty;
    private CallsignInfo CallInfo { get; set; } = new CallsignInfo();
    public async Task LookupClick(MouseEventArgs e)
    {
        if (Callsign.IsNullOrEmpty() == true)
        {
            Toaster.Add("Callsign is required", MatToastType.Warning, "title");
            return;
        }
        //Toaster.Add("Toaster test " + Callsign, MatToastType.Danger, "title");
        CallInfo = await LookupService.GetCallsignDetailsAsync(Callsign);
        if (CallInfo.HttpStatus == 200)
        {
            Toaster.Add("Callsign found", MatToastType.Success, "title");
        }
        else
        {
            Toaster.Add("Callsign not found", MatToastType.Danger, "title");
        }
    }

    private async Task ReplaceNotesClick(MouseEventArgs arg)
    {
        if (Callsign.IsNullOrEmpty() == true)
        {
            
            Toaster.Add("Callsign is required", MatToastType.Warning, "title");
            return;
        }
        CallInfo = await LookupService.PostDataAsync(new CallUpdateDTO { Callsign = Callsign, Note = Notes });
        Notes = string.Empty;

    }

    private async Task AddNotesClick()
    {
        if (Callsign.IsNullOrEmpty() == true)
        {

            Toaster.Add("Callsign is required", MatToastType.Warning, "title");
            return;
        }
        CallInfo = await LookupService.PutDataAsync(new CallUpdateDTO { Callsign = Callsign, Note = Notes });
        Notes = string.Empty;
    }
}
