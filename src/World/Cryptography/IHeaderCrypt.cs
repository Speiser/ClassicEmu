namespace Classic.World.Cryptography
{
    public interface IHeaderCrypt
    {
        byte[] Decrypt(byte[] data);
        byte[] Encrypt(byte[] data);
    }
}
