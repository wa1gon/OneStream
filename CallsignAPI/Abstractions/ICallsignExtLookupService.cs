using CallSignCommon.Models;


namespace CallsignAPI.Abstractions;

public interface ICallsignExtLookupService
{
    Task<CallsignInfo> GetCallsignDetailsAsync(string callsign);
    bool IsCallsignValid(string callsign);
}
