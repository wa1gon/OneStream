namespace CallsignAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CallSignLookupController: ControllerBase
{
    private readonly IRepoService _repo;

    public CallSignLookupController( IRepoService repo)
    {
        _repo = repo;
    }
    [HttpGet(Name = "Lookup")]
    public async Task<ActionResult<CallsignInfo>> Get([FromQuery] string callsign)
    {
        var result = await _repo.GetCallsignDetailsAsync(callsign);
        return result;
    }
    [HttpPost(Name = "update")]
    public async  Task<IActionResult> Post( [FromBody] CallUpdateDTO callUpdate)
    {
        var result = await _repo.GetCallsignDetailsAsync(callUpdate.Callsign);
        if (result.Status.Equals("VALID"))
        {
            result.Notes = result.Notes + "; " +callUpdate.Note;
            return Ok();
        }
        return BadRequest("Invalid Callsign");
    }
}
