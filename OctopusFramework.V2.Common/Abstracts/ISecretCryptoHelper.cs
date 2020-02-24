using System;
using System.Collections.Generic;
using System.Text;

namespace OctopusFramework.V2.Common
{
    public interface ISecretCryptoHelper : ITwoWayCryptoHelper
    {
        void SetSecret(string SecretString);
    }
}
