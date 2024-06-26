﻿using CallSignCommon.Models;


namespace OneStream.Abstractions;

public interface ILookupService
{
    Task<CallsignInfo> GetCallsignDetailsAsync(string callsign);
    Task<CallsignInfo> PostDataAsync(CallUpdateDTO NotesData);
    Task<CallsignInfo> PutDataAsync(CallUpdateDTO NotesData);
    bool IsCallsignValid(string callsign);
}
