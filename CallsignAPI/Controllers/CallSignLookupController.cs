﻿namespace CallsignAPI.Controllers;

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
    [HttpPost(Name = "addNote")]
    public async  Task<CallsignInfo> Post( [FromBody] CallUpdateDTO callUpdate)
    {
        var result = await _repo.ReplaceNoteInCache(callUpdate);
        return result;
    }
    [HttpPut(Name = "updateNote")]
    public async Task<CallsignInfo> Put([FromBody] CallUpdateDTO callUpdate)
    {
        var result = await _repo.AddNoteInCache(callUpdate);
        return result;
    }
}
