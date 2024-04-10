using CallsignAPI.Abstractions;
using CallSignCommon.Models;
using System.Text.Json;
using CallSignCommon.ExtensionMethods;
using System.Text.RegularExpressions;

namespace CallsignAPI.Services;

public class CallsignExtLookupService: ICallsignExtLookupService
{
    const string LookupUrlTemplate = "https://callook.info/{callsign}/json";
    private HttpClient _httpClient;
    public async Task<CallsignInfo> GetCallsignDetailsAsync(string callsign)
    {
        try
        {
            if (IsCallsignValid(callsign) == false)
            {
                CallsignInfo statusError = new CallsignInfo();
                statusError.HttpStatus = 400;
                return statusError;
            }

            _httpClient = new HttpClient();
            string LookupUrl = LookupUrlTemplate.Replace("{callsign}", callsign);   
            HttpResponseMessage response = await _httpClient.GetAsync(LookupUrl);

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };
                string content = await response.Content.ReadAsStringAsync();
                CallsignInfo callsignDetails = JsonSerializer.Deserialize<CallsignInfo>(content,options);
                return callsignDetails;
            }
            else
            {
                CallsignInfo statusError = new CallsignInfo();
                statusError.HttpStatus = (int)response.StatusCode;

                return statusError;
            }
        }
        catch (Exception ex)
        {
            CallsignInfo statusError = new CallsignInfo();
            statusError.HttpStatus = -1;
            statusError.Exception = ex;
            return statusError;
        }
    }

    private bool IsCallsignValid(string callsign)
    {
        if (callsign.IsNullOrEmpty() )
        {
            return false;
        }
        if (callsign.Length < 3) 
        {
            return false;
        }
        if (Regex.IsMatch(callsign, @"^[A-Za-z]*\d+[A-Za-z]*$") == false)
        {
            return false;
        }   
        return true;
    }
}
