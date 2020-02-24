namespace OctopusFramework.V2.Basis
{
    public interface IOneWayCryptoHelper
    {
        string Encrypt(string keyString);
        bool ValidateCheck(string targetHash, string keyString);
    }
}
