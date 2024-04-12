using System.Text;

namespace OneStream.Services;

public class LookupService: ILookupService
{
    //TODO: Move this to appsettings.json
    const string LookupHost = "https://localhost:7069/CallSignLookup";
    const string LookupUrlTemplate = $"{LookupHost}?callsign=";

    private HttpClient _httpClient;
    private JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };
    public const string ERROR = "ERROR";

    //public LookupService(HttpClient httpClient)
    //{
    //    _httpClient = httpClient;

    //}
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

            _httpClient = new HttpClient();
            var lookupUrl = LookupUrlTemplate + callsign;
            HttpResponseMessage response = await _httpClient.GetAsync(lookupUrl);

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };
                var content = await response.Content.ReadAsStringAsync();
                var callsignDetails = JsonSerializer.Deserialize<CallsignInfo>(content, options);
                callsignDetails.HttpStatus = (int)response.StatusCode;
                return callsignDetails;
            }
            else
            {
                var statusError = new CallsignInfo
                {
                    HttpStatus = (int)response.StatusCode,
                    Status = ERROR
                };

                return statusError;
            }
        }
        catch (Exception ex)
        {
            var statusError = new CallsignInfo
            {
                HttpStatus = (int)HttpStatusCode.InternalServerError,
                Status = ERROR,
                Exception = ex
            };
            return statusError;
        }
    }

    //public async Task<CallsignInfo> GetCallsignDetailsAsync(string callsign)
    //{
    //    try
    //    {
    //        if (IsCallsignValid(callsign) == false)
    //        {
    //            CallsignInfo statusError = new CallsignInfo
    //            {
    //                HttpStatus = (int)HttpStatusCode.BadRequest,
    //                Status = ERROR
    //            };
    //            return statusError;
    //        }
    //        _httpClient = new HttpClient();
    //    } 
    //    catch(Exception ex)
    //    {
    //        var statusError = new CallsignInfo
    //        {
    //            HttpStatus = (int)HttpStatusCode.InternalServerError,
    //            Status = ERROR,
    //            Exception = ex
    //        };
    //        return statusError;
    //    }
    //}
    public async Task<CallsignInfo> PostDataAsync(CallUpdateDTO NotesData)
    {
        try
        {
            var jsonData = JsonSerializer.Serialize(NotesData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Post the data
            HttpResponseMessage response = await _httpClient.PostAsync(LookupHost, content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<CallsignInfo>(responseContent,_jsonOptions);
            }
            else
            {
                var errorInfo = new CallsignInfo
                {
                    HttpStatus = (int)response.StatusCode,
                    Status = ERROR
                };
                return errorInfo;
            }

        }
        catch (HttpRequestException e)
        {
            // Handle potential network errors here
            Console.WriteLine($"Error posting data: {e.Message}");
            throw;
        }
        catch (Exception e)
        {
            // Handle other potential errors here
            Console.WriteLine($"Error posting data: {e.Message}");
            throw;
        }
    }
    //TODO: Move this to a common library
    public bool IsCallsignValid(string callsign)
    {
        if (callsign.IsNullOrEmpty())
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

