namespace CallSignCommon.Models;

public class CallsignDto
{
    public string status { get; set; }
    public string type { get; set; }
    public Current current { get; set; }
    public Previous previous { get; set; }
    public Trustee trustee { get; set; }
    public string name { get; set; }
    public Address address { get; set; }
    public Location location { get; set; }
    public Otherinfo otherInfo { get; set; }
}
