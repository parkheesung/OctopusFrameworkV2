namespace OctopusFramework.V2.Common
{
    public interface IOneWayCryptoHelper
    {
        string Encrypt(string keyString);
        bool ValidateCheck(string targetHash, string keyString);
    }
}
