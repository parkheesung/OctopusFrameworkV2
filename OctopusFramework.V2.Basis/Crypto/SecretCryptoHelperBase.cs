namespace OctopusFramework.V2.Crypto
{
    public class SecretCryptoHelperBase : CryptoHelperBase
    {
        protected string SecretKey { get; set; } = string.Empty;


        public virtual void SetSecret(string SecretString)
        {
            this.SecretKey = SecretString;
        }
    }
}
