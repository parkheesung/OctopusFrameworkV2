namespace OctopusFramework.V2.Basis
{
    public interface ITwoWayCryptoHelper
    {
        string Decrypt(string keyString);
        string Encrypt(string keyString);
    }
}
