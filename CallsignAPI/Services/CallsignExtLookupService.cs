namespace CallsignAPI.Services;

public class CallsignExtLookupService: ICallsignExtLookupService
{
    const string LookupUrlTemplate = "https://callook.info/{callsign}/json";
    private HttpClient _httpClient;
    public const string ERROR = "ERROR";
    public async Task<CallsignInfo> GetCallsignDetailsAsync(string callsign)
    {
        try
        {
            if (IsCallsignValid(callsign) == false)
            {
                CallsignInfo statusError = new CallsignInfo
                {
                    HttpStatus = 400
                };
                return statusError;
            }

            _httpClient = new HttpClient();
            string lookupUrl = LookupUrlTemplate.Replace("{callsign}", callsign);   
            HttpResponseMessage response = await _httpClient.GetAsync(lookupUrl);

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
            CallsignInfo statusError = new CallsignInfo
            {
                HttpStatus = -1,
                Status = ERROR,
                Exception = ex
            };
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
        return Regex.IsMatch(callsign, @"^[A-Za-z]*\d+[A-Za-z]*$") != false;
    }
}
