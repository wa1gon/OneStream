namespace CallsignAPI.Services;
using System.Collections.Concurrent;


public class RepoService : IRepoService
{
    private ConcurrentDictionary<string, CallsignInfo> _cache = new ConcurrentDictionary<string, CallsignInfo>();
    private ICallsignExtLookupService _lookupServ;  
    public RepoService(ICallsignExtLookupService lookupServ)
    {
        _lookupServ = lookupServ;
    }
    public void ClearCache()
    {
        _cache.Clear();
    }
    public void AddToCache(string callsign, CallsignInfo info)
    {
        _cache.TryAdd(callsign, info);
    }
    public async Task<CallsignInfo> ReplaceNoteInCache(CallUpdateDTO updateDto)
    {
        var info = await GetCallsignDetailsAsync(updateDto.Callsign);
        if (info.Status == "VALID")
        {
            info.Notes = updateDto.Note;
        }
        return info;
    }
    public async Task<CallsignInfo> AddNoteInCache(CallUpdateDTO updateDto)
    {
        var info = await GetCallsignDetailsAsync(updateDto.Callsign);
        if (info.Status == "VALID")
        {
            if (info.Notes.IsNullOrEmpty())
                info.Notes = updateDto.Note;
            else
            {
                info.Notes = info.Notes + "; " + updateDto.Note;
            }
        }
        return info;
    }
    public CallsignInfo GetFromCache(string callsign)
    {
        CallsignInfo info;
        _cache.TryGetValue(callsign, out info!);
        return info;
    }
    public async Task<CallsignInfo> GetCallsignDetailsAsync(string callsign)
    {
        callsign = callsign.ToUpper();
        CallsignInfo callInfo = GetFromCache(callsign);
        if (callInfo is not null)
        {
            callInfo.CacheHitCount++;
            return callInfo;
        }

        callInfo = await _lookupServ.GetCallsignDetailsAsync(callsign);
        if (callInfo.Status.Equals("VALID"))
        {
            callInfo.CacheHitCount = 0;
            AddToCache(callsign, callInfo);
        }

        return callInfo;
    }
    public void RemoveFromCache(string callsign)
    {
        if (_lookupServ.IsCallsignValid(callsign) == false)
            return;
        CallsignInfo info;
        _cache.TryRemove(callsign, out info!);
    }
}
