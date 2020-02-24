using System;
using System.Collections.Generic;
using System.Text;

namespace OctopusFramework.V2.Common
{
    public interface IDualCryptoHelper
    {
        void SetPublicSecret(string publicString);
        void SetPrivateSecret(string privateString);
    }
}
