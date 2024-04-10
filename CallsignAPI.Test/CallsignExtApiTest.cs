namespace CallsignAPI.Test;

public class CallsignExtApiTest
{
    [Fact]
    public async Task ApiTestShouldReturnWA1GONCallInfomation()
    {
        ICallsignExtLookupService sut = new CallsignExtLookupService(); 

        var callInfo = await sut.GetCallsignDetailsAsync("WA1GON");

        Assert.Equal("EM26ui", callInfo.Location.Gridsquare);
    }
    [Fact]
    public async Task ApiTestShouldReturn400OnEmptyCallsign()
    {
        ICallsignExtLookupService sut = new CallsignExtLookupService();
        var callInfo =  await sut.GetCallsignDetailsAsync("");
        Assert.Equal(400, (await sut.GetCallsignDetailsAsync("")).HttpStatus);
    }
    [Fact]
    public async Task ApiTestShouldReturn400OnInvalidCallsign()
    {
        ICallsignExtLookupService sut = new CallsignExtLookupService();
        var callInfo = await sut.GetCallsignDetailsAsync("wagon");
        Assert.Equal(400, callInfo.HttpStatus);
    }
    [Fact]
    public async Task ApiTestShouldReturnInvalidOnUnassignedCallsign()
    {
        ICallsignExtLookupService sut = new CallsignExtLookupService();
        var callInfo = await sut.GetCallsignDetailsAsync("xx1xx");
        Assert.Equal("INVALID", callInfo.Status);
    }
}