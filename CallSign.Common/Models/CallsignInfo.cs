namespace CallSignCommon.Models;

public class CallsignInfo
{
    public string Status { get; set; }
    public string Type { get; set; }
    public Current Current { get; set; }
    public Previous Previous { get; set; }
    public Trustee Trustee { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
    public Location Location { get; set; }
    public Otherinfo OtherInfo { get; set; }
    public int HttpStatus { get; set; }
    public Exception? Exception { get; set; } = null;
}
