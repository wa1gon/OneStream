namespace CallSignCommon.Models;

public class CallsignInfo
{
    public string Status { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public Current Current { get; set; } = new Current();
    public Previous Previous { get; set; } = new Previous();
    public Trustee Trustee { get; set; } = new Trustee();
    public string Name { get; set; }
    public Address Address { get; set; } = new Address();
    public Location Location { get; set; } = new Location();
    public Otherinfo OtherInfo { get; set; } = new Otherinfo();
    public int HttpStatus { get; set; }
    public Exception? Exception { get; set; } = null;

    public string Notes { get; set; } = string.Empty;
    public int CacheHitCount { get; set; } = 0;
}
