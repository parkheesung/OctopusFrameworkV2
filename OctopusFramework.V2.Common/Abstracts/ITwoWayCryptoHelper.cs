namespace OctopusFramework.V2.Common
{
    public interface ITwoWayCryptoHelper
    {
        string Decrypt(string keyString);
        string Encrypt(string keyString);
    }
}
