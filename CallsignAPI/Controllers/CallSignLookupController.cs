namespace CallsignAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CallSignLookupController
{
    private readonly ILogger<CallSignLookupController> _logger;
    private readonly IRepoService _repo;

    public CallSignLookupController(ILogger<CallSignLookupController> logger, IRepoService repo)
    {
        _logger = logger;
        _repo = repo;
    }
    [HttpGet(Name = "Lookup")]
    public async Task<ActionResult<CallsignInfo>> Get([FromQuery] string callsign)
    {
        var result = await _repo.GetCallsignDetailsAsync(callsign);
        return result;
    }
    [HttpPost(Name = "Update")]
    public CallsignInfo? Post()
    {
        return null;
    }
}
