using CallSignCommon.Models;
using Moq;

namespace CallsignAPI.Test;
public class CacheServiceTest
{
    [Fact]
    public async Task CacheTestShouldReturnWA1GONCallInfomation()
    {
        var mockService = new Mock<ICallsignExtLookupService>();
        mockService.Setup(service => service.GetCallsignDetailsAsync("WA1GON"))
            .ReturnsAsync(new CallsignInfo()
            {
                Current = new Current() 
                {
                    Callsign = "WA1GON"
                }
            })
            .Verifiable(); // Mark this setup as verifiable

        var sut = new RepoService(mockService.Object);

        // Act
        var result = await sut.GetCallsignDetailsAsync("WA1GON"); 
        mockService.Verify(); 
        Assert.Equal("WA1GON", result.Current.Callsign);
    }

    [Fact]
    public async Task CacheTestShouldReturnWA1GONCallAndBumpCache()
    {
        var mockService = new Mock<ICallsignExtLookupService>();
        mockService.Setup(service => service.GetCallsignDetailsAsync("WA1GON"))
            .ReturnsAsync(new CallsignInfo()
            {
                Current = new Current()
                {
                    Callsign = "WA1GON"
                },
                Status = "VALID"
            })
            .Verifiable(); 

        var sut = new RepoService(mockService.Object);

        // Act
        var result = await sut.GetCallsignDetailsAsync("WA1GON");
        mockService.Verify();
        result = await sut.GetCallsignDetailsAsync("WA1GON");
        Assert.Equal(1, result.CacheHitCount);

    }
}
