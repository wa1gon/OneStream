﻿namespace CallsignAPI.Abstractions;

public interface IRepoService
{
    void ClearCache();
    void AddToCache(string callsign, CallsignInfo info);
    CallsignInfo GetFromCache(string callsign);
    Task<CallsignInfo> GetCallsignDetailsAsync(string callsign);
    void RemoveFromCache(string callsign);
}