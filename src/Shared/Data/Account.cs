namespace Classic.Shared.Data;

public class Account
{
    public int Id { get; set; }
    public string Username { get; set; }
    public byte[] SessionKey { get; set; }
}
