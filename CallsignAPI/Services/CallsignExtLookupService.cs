using System.Net;

namespace CallsignAPI.Services;


// API Reference for Callook.info
// https://callook.info/api_reference.php

public sealed class CallsignExtLookupService: ICallsignExtLookupService
{
    const string LookupUrlTemplate = "https://callook.info/{callsign}/json";

    private readonly IHttpClientFactory _httpClientFactory;

    public const string ERROR = "ERROR";

    public CallsignExtLookupService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<CallsignInfo> GetCallsignDetailsAsync(string callsign)
    {
        try
        {
            if (IsCallsignValid(callsign) == false)
            {
                CallsignInfo statusError = new CallsignInfo
                {
                    HttpStatus = (int)HttpStatusCode.BadRequest,
                    Status = ERROR
                };
                return statusError;
            }

            var httpClient = _httpClientFactory.CreateClient();
            string lookupUrl = LookupUrlTemplate.Replace("{callsign}", callsign);   
            HttpResponseMessage response = await httpClient.GetAsync(lookupUrl);

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };
                string content = await response.Content.ReadAsStringAsync();
                CallsignInfo callsignDetails = JsonSerializer.Deserialize<CallsignInfo>(content,options);
                callsignDetails.HttpStatus = (int)response.StatusCode;
                return callsignDetails;
            }
            else
            {
                CallsignInfo statusError = new CallsignInfo();
                statusError.HttpStatus = (int)response.StatusCode;
                statusError.Status = ERROR;

                return statusError;
            }
        }
        catch (Exception ex)
        {
            CallsignInfo statusError = new CallsignInfo
            {
                HttpStatus = (int)HttpStatusCode.InternalServerError,
                Status = ERROR,
                Exception = ex
            };
            return statusError;
        }
    }
    public bool IsCallsignValid(string callsign)
    {
        if (callsign.IsNullOrEmpty() )
        {
            return false;
        }
        if (callsign.Length < 3) 
        {
            return false;
        }
        return Regex.IsMatch(callsign, @"^[A-Za-z]*\d+[A-Za-z]*$") != false;
    }
}
