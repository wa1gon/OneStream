using CallSignCommon.Models;


namespace OneStream.Abstractions;

public interface ILookupService
{
    Task<CallsignInfo> GetCallsignDetailsAsync(string callsign);
    bool IsCallsignValid(string callsign);
}
