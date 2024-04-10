using Microsoft.AspNetCore.Mvc;
using CallSignCommon.Models;
namespace CallsignAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CallSignLookupController
{
    private readonly ILogger<CallSignLookupController> _logger;

    public CallSignLookupController(ILogger<CallSignLookupController> logger)
    {
        _logger = logger;
    }
    [HttpGet(Name = "Lookup")]
    public CallsignInfo? Get()
    {
        return null;
    }
}
